using Endurist.Contracts;
using Endurist.Contracts.Profiles.Queries;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using Endurist.Models.Profiles;
using SideEffect.Extensions;
using SideEffect.Messaging.RPC;
using System.Linq.Expressions;

namespace Endurist.Reader.Handlers.Profiles;

internal sealed class GetProfilesQueryHandler : RequestHandlerBase<GetProfilesQuery, QueryPageReply<ProfilePreviewModel>>
{
    private readonly Storage _storage;

    public GetProfilesQueryHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task<QueryPageReply<ProfilePreviewModel>> HandleAsync(GetProfilesQuery message, CancellationToken cancellationToken = default)
    {
        Expression<Func<ProfileDocument, ProfilePreviewModel>> mapper = document =>
            new ProfilePreviewModel
            {
                Id = document.Id.ToString(),
                Name = document.Name
            };

        var filter = new ProfileFilter();
        var queryFilter = _storage.Profiles.BuildFilter(filter);

        var items = await _storage.Profiles.SearchAsync(mapper, queryFilter, message.Paging, message.Sorting, cancellationToken);
        if (items.IsEmpty())
        {
            return new QueryPageReply<ProfilePreviewModel> { Data = items, Paging = message.Paging, Sorting = message.Sorting };
        }

        var activityFilter = new ActivityFilter { ProfileIdIn = items.Select(x => x.Id.ToString()).ToList() };
        var activityQueryFilter = _storage.Activities.BuildFilter(activityFilter);

        Expression<Func<ActivityDocument, ProfilePreviewModel>> activityMapper = x =>
            new ProfileModel
            {
                Id = x.Id.ToString(),
                Distance = x.Distance,
                Duration = x.Duration
            };

        var profiles = await _storage.Activities.GetActivitySetTotalAsync(x => x.ProfileId, activityMapper, filter: activityQueryFilter, cancellationToken: cancellationToken);

        foreach (var item in items)
        {
            var summary = profiles.FirstOrDefault(x => x.Id == item.Id);
            if (summary is null)
            {
                continue;
            }

            item.Distance = summary.Distance;
            item.Duration = summary.Duration;
        }

        return new QueryPageReply<ProfilePreviewModel> { Data = items, Paging = message.Paging, Sorting = message.Sorting };
    }
}
