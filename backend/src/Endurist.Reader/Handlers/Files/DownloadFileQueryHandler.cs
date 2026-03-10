using Endurist.Contracts;
using Endurist.Contracts.Exceptions;
using Endurist.Contracts.Files.Queries;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Models.Files;
using SideEffect.Messaging.RPC;

namespace Endurist.Reader.Handlers.Files;

internal sealed class DownloadFileQueryHandler : RequestHandlerBase<DownloadFileQuery, QueryReply<FileDownloadModel>>
{
    private readonly Storage _storage;

    public DownloadFileQueryHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task<QueryReply<FileDownloadModel>> HandleAsync(DownloadFileQuery message, CancellationToken cancellationToken = default)
    {
        var file = await _storage.Files.GetByIdAsync(message.FileId, cancellationToken)
            ?? throw new EntityNotFoundException(typeof(FileDocument), message.FileId);

        var filePath = Path.GetTempFileName();

        using (var memoryStream = new MemoryStream())
        {
            await memoryStream.WriteAsync(Convert.FromBase64String(file.Content), cancellationToken);
            memoryStream.Seek(0, SeekOrigin.Begin);

            using var stream = File.Create(filePath);
            await memoryStream.CopyToAsync(stream, cancellationToken);
        }

        var data = new FileDownloadModel { Name = file.Name, Extension = file.Extension, FilePath = filePath };

        return new QueryReply<FileDownloadModel> { Data = data };
    }
}
