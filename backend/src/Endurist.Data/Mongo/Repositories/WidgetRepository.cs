using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using Endurist.Hosting.Settings;
using MongoDB.Driver;

namespace Endurist.Data.Mongo.Repositories;

public class WidgetRepository : RepositoryBase<WidgetDocument>
{
    protected override string CollectionName => "widgets";

    public WidgetRepository(MongoStorageConfiguration settings)
        : base(settings) { }

    public FilterDefinition<WidgetDocument> BuildFilter(WidgetFilter filter)
    {
        var queryFilter = Builders<WidgetDocument>.Filter.Empty;

        return queryFilter;
    }
}
