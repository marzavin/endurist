using Endurist.Contracts;
using Endurist.Contracts.Files.Queries;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using Endurist.Models.Files;
using SideEffect.Messaging.RPC;
using System.Linq.Expressions;

namespace Endurist.Reader.Handlers.Files;

internal sealed class GetFilesQueryHandler : RequestHandlerBase<GetFilesQuery, QueryPageReply<FilePreviewModel>>
{
    private readonly Storage _storage;

    public GetFilesQueryHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task<QueryPageReply<FilePreviewModel>> HandleAsync(GetFilesQuery message, CancellationToken cancellationToken = default)
    {
        Expression<Func<FileDocument, FilePreviewModel>> mapper = document =>
            new FilePreviewModel
            {
                Id = document.Id.ToString(),
                Name = document.Name,
                Extension = document.Extension,
                Size = document.Size,
                Status = document.Status,
                UploadedAt = document.UploadedAt,
                ProcessedAt = document.ProcessedAt,
                ActivityStartedAt = document.ActivityStartedAt
            };

        var filter = new FileFilter();
        var queryFilter = _storage.Files.BuildFilter(filter);

        var items = await _storage.Files.SearchAsync(mapper, queryFilter, message.Paging, message.Sorting, cancellationToken);
        return new QueryPageReply<FilePreviewModel> { Data = items, Paging = message.Paging, Sorting = message.Sorting };
    }
}
