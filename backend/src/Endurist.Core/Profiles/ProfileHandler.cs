using Endurist.Common.Models;
using Endurist.Core.Profiles.Models;
using Endurist.Core.Profiles.Requests;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using MediatR;
using SideEffect.Extensions;
using System.Linq.Expressions;

namespace Endurist.Core.Profiles;

public class ProfileHandler : HandlerBase,
    IRequestHandler<GetProfilesRequest, DataPageResponse<ProfilePreviewModel>>,
    IRequestHandler<GetProfileRequest, DataResponse<ProfileModel>>
{
    public ProfileHandler(Storage storage)
        : base(storage)
    { }

    public async Task<DataPageResponse<ProfilePreviewModel>> Handle(GetProfilesRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<ProfileDocument, ProfilePreviewModel>> mapper = document =>
            new ProfilePreviewModel
            {
                Id = document.Id.ToString(),
                Name = document.Name
            };

        var filter = new ProfileFilter();
        var queryFilter = Storage.Profiles.BuildFilter(filter);

        var items = await Storage.Profiles.SearchAsync(mapper, queryFilter, request.Paging, request.Sorting, cancellationToken);
        if (items.IsEmpty())
        {
            return new DataPageResponse<ProfilePreviewModel> { Data = items, Paging = request.Paging };
        }

        var activityFilter = new ActivityFilter { ProfileIdIn = items.Select(x => x.Id.ToString()).ToList() };
        var activityQueryFilter = Storage.Activities.BuildFilter(activityFilter);

        Expression<Func<ActivityDocument, ProfilePreviewModel>> activityMapper = x =>
            new ProfileModel
            {
                Id = x.Id.ToString(),
                Distance = x.Distance,
                Duration = x.Duration
            };

        var profiles = await Storage.Activities.GetActivitySetTotalAsync(x => x.ProfileId, activityMapper, filter: activityQueryFilter, cancellationToken: cancellationToken);

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

        return new DataPageResponse<ProfilePreviewModel> { Data = items, Paging = request.Paging };
    }

    public async Task<DataResponse<ProfileModel>> Handle(GetProfileRequest request, CancellationToken cancellationToken)
    {
        var document = await Storage.Profiles.GetByIdAsync(request.Id, cancellationToken);
        if (document is null)
        {
            return new DataResponse<ProfileModel> { Data = null };
        }

        var filter = new ActivityFilter { ProfileIdIn = [request.Id] };
        var queryFilter = Storage.Activities.BuildFilter(filter);

        Expression<Func<ActivityDocument, ProfileModel>> mapper = x =>
            new ProfileModel
            {
                Id = x.ProfileId.ToString(),
                Distance = x.Distance,
                Duration = x.Duration
            };

        var profiles = await Storage.Activities.GetActivitySetTotalAsync(x => x.ProfileId, mapper, filter: queryFilter, cancellationToken: cancellationToken);

        var data = profiles.FirstOrDefault();
        if (data is not null)
        {
            data.Id = document.EntityId;
            data.Name = document.Name;
        }

        //filter.DistanceGte = Constants.HalfMarathonDistance;
        //queryFilter = Storage.Activities.BuildFilter(filter);
        //var halfMarathonActivities = await Storage.Activities.SearchAsync(x => x, queryFilter, cancellationToken: cancellationToken);

        //var halfMarathonProgressFeature = new DistanceProgressFeature(Constants.HalfMarathonDistance);
        //var halfMarathonProrgessModel = await halfMarathonProgressFeature.BuildModelAsync(halfMarathonActivities);

        //data.DistanceProgress = [halfMarathonProrgessModel];

        return new DataResponse<ProfileModel> { Data = data };
    }
}