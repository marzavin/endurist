using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using Endurist.Hosting.Settings;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Endurist.Data.Mongo.Repositories;

public class ProfileRepository : RepositoryBase<ProfileDocument>
{
    protected override string CollectionName => "profiles";

    public ProfileRepository(MongoStorageConfiguration settings)
        : base(settings) { }

    protected override Dictionary<string, Expression<Func<ProfileDocument, object>>> SortingFields =>
        new()
        {
            { nameof(ProfileDocument.Name).ToUpperInvariant(), x => x.Name }
        };

    public FilterDefinition<ProfileDocument> BuildFilter(ProfileFilter filter)
    {
        var queryFilter = Builders<ProfileDocument>.Filter.Empty;

        return queryFilter;
    }
}