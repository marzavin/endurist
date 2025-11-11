using Endurist.Common.Models;
using Endurist.Core.Activities.Models;
using MediatR;

namespace Endurist.Core.Activities.Requests;

public class GetSegmentRequest : IRequest<DataResponse<SegmentModel>>
{
    public GetSegmentRequest(string activityId, int segmentIndex)
    {
        ActivityId = activityId;
        SegmentIndex = segmentIndex;
    }

    public string ActivityId { get; }

    public int SegmentIndex { get; }
}
