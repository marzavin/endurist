using Endurist.Common.Models;
using Endurist.Core.Files.Models;
using Endurist.Core.Files.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Web.Controllers;

/// <summary>
/// API controller to handle file requests.
/// </summary>
[ApiController]
[Authorize]
[Route("api/files")]
[Produces("application/json")]

public class FileController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes new instance of <see cref="FileController"/>.
    /// </summary>
    /// <param name="mediator">See <see cref="IMediator"/> for more information.</param>
    /// <exception cref="ArgumentNullException">Throws an exception in case of any parameter is null.</exception>
    public FileController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// GET request to retrieve the list of source files.
    /// </summary>
    /// <param name="paging">See <see cref="Paging"/> for more information.</param>
    /// <param name="sorting">See <see cref="SortingInfo"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="FilePreviewModel"/> for more information.</returns>
    [HttpGet]
    [ProducesResponseType<DataPageResponse<FilePreviewModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFilesAsync(
        [FromQuery] PagingInfo paging,
        [FromQuery] SortingInfo sorting,
        CancellationToken cancellationToken = default)
    {
        var request = new GetFilesRequest(paging, sorting);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// POST request to upload new source file.
    /// </summary>
    /// <param name="file">See <see cref="IFormFile"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="FileUploadModel"/> for more information.</returns>
    [HttpPost("upload")]
    [ProducesResponseType<DataResponse<FileUploadModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadFileAsync(
        IFormFile file,
        CancellationToken cancellationToken = default)
    {
        var filePath = Path.GetTempFileName();

        using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        var request = new UploadFileRequest(file.FileName, filePath);
        var response = await _mediator.Send(request, cancellationToken);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        return Ok(response);
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
        var request = new DownloadFileRequest(fileId);
        var response = await _mediator.Send(request, cancellationToken);

        var bytes = await System.IO.File.ReadAllBytesAsync(response.Data.FilePath, cancellationToken);
        System.IO.File.Delete(response.Data.FilePath);

        return File(bytes, "application/xml", fileDownloadName: $"{response.Data.Name}.{response.Data.Extension}");
    }
}