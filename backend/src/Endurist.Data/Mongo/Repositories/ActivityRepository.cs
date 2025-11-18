using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using Endurist.Hosting.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using SideEffect.Extensions;
using System.Linq.Expressions;

namespace Endurist.Data.Mongo.Repositories;

public class ActivityRepository : RepositoryBase<ActivityDocument>
{
    protected override string CollectionName => "activities";

    public ActivityRepository(MongoStorageConfiguration settings)
        : base(settings) { }

    protected override Dictionary<string, Expression<Func<ActivityDocument, object>>> SortingFields =>
        new(StringComparer.OrdinalIgnoreCase)
        {
            { nameof(ActivityDocument.StartTime), x => x.StartTime }
        };

    public Task<List<TModel>> GetActivitySetTotalAsync<TModel>(Expression<Func<ActivityDocument, ObjectId>> key, Expression<Func<ActivityDocument, TModel>> mapper, FilterDefinition<ActivityDocument> filter = null, CancellationToken cancellationToken = default)
    {
        var queryFilter = filter ?? Builders<ActivityDocument>.Filter.Empty;

        var builder = Builders<ActivityDocument>.Projection;
        var projection = builder.Expression(mapper);

        var pipeline = new EmptyPipelineDefinition<ActivityDocument>()
            .Match(queryFilter)
            .Group(key,
                grouping => new ActivityDocument
                {
                    Id = grouping.Key,
                    Duration = grouping.Sum(x => x.Duration),
                    Distance = grouping.Sum(x => x.Distance)
                }
            )
            .Project(projection);

        var search = Collection.Aggregate(pipeline, cancellationToken: cancellationToken);
        
        return search.ToListAsync(cancellationToken);
    }
    
    public FilterDefinition<ActivityDocument> BuildFilter(ActivityFilter filter)
    {
        var queryFilter = Builders<ActivityDocument>.Filter.Empty;

        if (!filter.FileIdEq.IsEmpty())
        {
            queryFilter &= Builders<ActivityDocument>.Filter.Eq(x => x.FileId, ObjectId.Parse(filter.FileIdEq));
        }

        if (!filter.ProfileIdIn.IsEmpty())
        {
            var identifiers = filter.ProfileIdIn.Select(ObjectId.Parse);
            queryFilter &= Builders<ActivityDocument>.Filter.In(x => x.ProfileId, identifiers);
        }
        
        if (!filter.CategoryIn.IsEmpty())
        {
            queryFilter &= Builders<ActivityDocument>.Filter.In(x => x.Category, filter.CategoryIn);
        }

        if (!filter.TypeIn.IsEmpty())
        {
            queryFilter &= Builders<ActivityDocument>.Filter.In(x => x.Type, filter.TypeIn);
        }

        if (filter.DistanceGte.HasValue)
        {
            queryFilter &= Builders<ActivityDocument>.Filter.Gte(x => x.Distance, filter.DistanceGte);
        }

        if (filter.StartTimeGte.HasValue)
        {
            queryFilter &= Builders<ActivityDocument>.Filter.Gte(x => x.StartTime, filter.StartTimeGte);
        }

        return queryFilter;
    }
}
