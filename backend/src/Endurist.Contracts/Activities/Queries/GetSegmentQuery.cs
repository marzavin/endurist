using Endurist.Models.Activities;

namespace Endurist.Contracts.Activities.Queries;

public class GetSegmentQuery : QueryBase<QueryReply<SegmentModel>>
{
    public GetSegmentQuery(string activityId, int segmentIndex)
    {
        ActivityId = activityId;
        SegmentIndex = segmentIndex;
    }

    public string ActivityId { get; }

    public int SegmentIndex { get; }
}
