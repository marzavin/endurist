using Endurist.Contracts;
using Endurist.Contracts.Activities.Queries;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using Endurist.Models.Activities;
using SideEffect.Messaging.RPC;
using System.Linq.Expressions;

namespace Endurist.Reader.Handlers.Activities;

internal sealed class GetActivitiesQueryHandler : RequestHandlerBase<GetActivitiesQuery, QueryPageReply<ActivityPreviewModel>>
{
    private readonly Storage _storage;

    public GetActivitiesQueryHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task<QueryPageReply<ActivityPreviewModel>> HandleAsync(GetActivitiesQuery message, CancellationToken cancellationToken = default)
    {
        Expression<Func<ActivityDocument, ActivityPreviewModel>> mapper = document =>
            new ActivityPreviewModel
            {
                Id = document.Id.ToString(),
                Category = document.Category,
                StartTime = document.StartTime,
                Distance = document.Distance,
                Duration = document.Duration
            };

        var filter = new ActivityFilter();
        var queryFilter = _storage.Activities.BuildFilter(filter);

        var items = await _storage.Activities.SearchAsync(mapper, queryFilter, message.Paging, message.Sorting, cancellationToken);
        return new QueryPageReply<ActivityPreviewModel> { Data = items, Paging = message.Paging, Sorting = message.Sorting };
    }
}
