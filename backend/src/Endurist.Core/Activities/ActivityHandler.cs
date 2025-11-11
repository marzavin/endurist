using Endurist.Common.Models;
using Endurist.Core.Activities.Mappers;
using Endurist.Core.Activities.Models;
using Endurist.Core.Activities.Requests;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Data.Mongo.Filters;
using MediatR;
using System.Linq.Expressions;
using ExecutionContext = Endurist.Core.Services.ExecutionContext;

namespace Endurist.Core.Activities;

public class ActivityHandler : HandlerBase,
    IRequestHandler<GetActivitiesRequest, DataPageResponse<ActivityPreviewModel>>,
    IRequestHandler<GetActivityRequest, DataResponse<ActivityModel>>,
    IRequestHandler<GetSegmentRequest, DataResponse<SegmentModel>>
{
    protected ExecutionContext Context { get; }

    public ActivityHandler(Storage storage, ExecutionContext context)
        : base(storage)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<DataPageResponse<ActivityPreviewModel>> Handle(GetActivitiesRequest request, CancellationToken cancellationToken)
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

        var filter = new ActivityFilter { ProfileIdIn = [Context.UserId] };
        var queryFilter = Storage.Activities.BuildFilter(filter);
        
        var items = await Storage.Activities.SearchAsync(mapper, queryFilter, request.Paging, request.Sorting, cancellationToken);
        return new DataPageResponse<ActivityPreviewModel> { Data = items, Paging = request.Paging };
    }
    
    public async Task<DataResponse<ActivityModel>> Handle(GetActivityRequest request, CancellationToken cancellationToken)
    {
        var activity = await Storage.Activities.GetByIdAsync(request.ActivityId, cancellationToken);

        var data = new ActivityModel();
        ActivityMapper.Map(activity, data);
               
        return new DataResponse<ActivityModel> { Data = data };
    }

    public async Task<DataResponse<SegmentModel>> Handle(GetSegmentRequest request, CancellationToken cancellationToken)
    {
        var activity = await Storage.Activities.GetByIdAsync(request.ActivityId, cancellationToken);
        var segment = activity.Segments.OrderBy(x => x.StartIndex).Skip(request.SegmentIndex - 1).First();

        var data = new SegmentModel();
        SegmentMapper.Map(segment, data, activity.Track);

        return new DataResponse<SegmentModel> { Data = data };
    }
}
