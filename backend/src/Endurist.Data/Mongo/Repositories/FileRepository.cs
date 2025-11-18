using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Enums;
using Endurist.Data.Mongo.Filters;
using Endurist.Hosting.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using SideEffect.Extensions;
using System.Linq.Expressions;

namespace Endurist.Data.Mongo.Repositories;

public class FileRepository : RepositoryBase<FileDocument>
{
    protected override string CollectionName => "files";

    public FileRepository(MongoStorageConfiguration settings)
        : base(settings) { }

    protected override Dictionary<string, Expression<Func<FileDocument, object>>> SortingFields =>
        new(StringComparer.OrdinalIgnoreCase)
        {
            { nameof(FileDocument.UploadedAt), x => x.UploadedAt },
            { nameof(FileDocument.ProcessedAt), x => x.ProcessedAt },
            { nameof(FileDocument.ActivityStartedAt), x => x.ActivityStartedAt }
        };

    public FilterDefinition<FileDocument> BuildFilter(FileFilter filter)
    {
        var queryFilter = Builders<FileDocument>.Filter.Empty;

        if (!filter.ProfileIdIn.IsEmpty())
        {
            var identifiers = filter.ProfileIdIn.Select(ObjectId.Parse);
            queryFilter &= Builders<FileDocument>.Filter.In(x => x.ProfileId, identifiers);
        }

        if (!filter.StatusIn.IsEmpty())
        {
            queryFilter &= Builders<FileDocument>.Filter.In(x => x.Status, filter.StatusIn);
        }

        if (!filter.HashEq.IsEmpty())
        {
            queryFilter &= Builders<FileDocument>.Filter.Eq(x => x.Hash, filter.HashEq);
        }

        return queryFilter;
    }

    public async Task<bool> DocumentExistsByHashAsync(string hash)
    {
        var queryFilter = Builders<FileDocument>.Filter.Eq(x => x.Hash, hash);
        return (await Collection.Find(queryFilter).CountDocumentsAsync()) > 0;
    }

    public async Task<FileDocument> SetStatusAndReturnAsync(FilterDefinition<FileDocument> filter, FileStatus status, CancellationToken cancellationToken)
    {
        var update = Builders<FileDocument>.Update.Set(x => x.Status, status);
        var options = new FindOneAndUpdateOptions<FileDocument> { ReturnDocument = ReturnDocument.After };

        return await Collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);
    }
}
