using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using Endurist.Hosting.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using SideEffect.Extensions;

namespace Endurist.Data.Mongo.Repositories;

public class ProfileWidgetRepository : RepositoryBase<ProfileWidgetDocument>
{
    protected override string CollectionName => "profileWidgets";

    public ProfileWidgetRepository(MongoStorageConfiguration settings)
        : base(settings)
    { }

    public FilterDefinition<ProfileWidgetDocument> BuildFilter(ProfileWidgetFilter filter)
    {
        var queryFilter = Builders<ProfileWidgetDocument>.Filter.Empty;

        if (!filter.ProfileIdEq.IsEmpty())
        {
            queryFilter &= Builders<ProfileWidgetDocument>.Filter.Eq(x => x.ProfileId, ObjectId.Parse(filter.ProfileIdEq));
        }

        if (!filter.WidgetIdEq.IsEmpty())
        {
            queryFilter &= Builders<ProfileWidgetDocument>.Filter.Eq(x => x.WidgetId, ObjectId.Parse(filter.WidgetIdEq));
        }

        return queryFilter;
    }
}
