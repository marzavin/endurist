using Endurist.Common.Models;
using Endurist.Core.Activities.Models;
using MediatR;

namespace Endurist.Core.Activities.Requests;

public class GetActivityRequest : IRequest<DataResponse<ActivityModel>>
{
    public GetActivityRequest(string activityId)
    {
        ActivityId = activityId;
    }

    public string ActivityId { get; }
}
