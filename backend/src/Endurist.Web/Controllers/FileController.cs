using Endurist.Contracts;
using Endurist.Contracts.Files.Commands;
using Endurist.Contracts.Files.Queries;
using Endurist.Models.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;
using SideEffect.Messaging;

namespace Endurist.Web.Controllers;

/// <summary>
/// API controller to handle file requests.
/// </summary>
[ApiController]
[Route("api/files")]
[Produces("application/json")]

public class FileController(ExecutionContext executionContext, IMessageHubClient hub) 
    : MessageHubControllerBase(executionContext, hub)
{
    /// <summary>
    /// GET request to retrieve the list of source files.
    /// </summary>
    /// <param name="paging">See <see cref="Paging"/> for more information.</param>
    /// <param name="sorting">See <see cref="SortingInfo"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="FilePreviewModel"/> for more information.</returns>
    [HttpGet]
    [ProducesResponseType<QueryPageReply<FilePreviewModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFilesAsync(
        [FromQuery] PagingInfo paging,
        [FromQuery] SortingInfo sorting,
        CancellationToken cancellationToken = default)
    {
        var query = new GetFilesQuery(paging, sorting);
        var reply = await Hub.ExecuteRequestAsync<GetFilesQuery, QueryPageReply<FilePreviewModel>>(query, cancellationToken);
        return Ok(reply);
    }

    /// <summary>
    /// POST request to upload new source file.
    /// </summary>
    /// <param name="file">See <see cref="IFormFile"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="FileUploadModel"/> for more information.</returns>
    [HttpPost("upload")]
    [Authorize]
    [ProducesResponseType<QueryReply<FileUploadModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadFileAsync(
        IFormFile file,
        CancellationToken cancellationToken = default)
    {
        var filePath = Path.GetTempFileName();

        using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        var command = new UploadFileCommand(file.FileName, filePath);
        await Hub.PublishEventAsync(command, cancellationToken);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        return NoContent();
    }

    /// <summary>
    /// GET request to download source file.
    /// </summary>
    /// <param name="fileId">Identifier of the file.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>Returns source file content.</returns>
    [HttpGet("{fileId:mongoId}/download")]
    public async Task<IActionResult> DownloadFileAsync(
        [FromRoute] string fileId,
        CancellationToken cancellationToken = default)
    {
        var query = new DownloadFileQuery(fileId);
        var reply = await Hub.ExecuteRequestAsync<DownloadFileQuery, QueryReply<FileDownloadModel>>(query, cancellationToken);

        var bytes = await System.IO.File.ReadAllBytesAsync(reply.Data.FilePath, cancellationToken);
        System.IO.File.Delete(reply.Data.FilePath);

        return File(bytes, "application/xml", fileDownloadName: $"{reply.Data.Name}.{reply.Data.Extension}");
    }
}