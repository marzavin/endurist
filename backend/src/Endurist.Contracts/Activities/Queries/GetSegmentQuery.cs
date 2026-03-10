using Endurist.Models.Activities;

namespace Endurist.Contracts.Activities.Queries;

public class GetSegmentQuery : QueryBase<QueryReply<SegmentModel>>
{
    public GetSegmentQuery(string userId, string activityId, int segmentIndex)
        : base(userId)
    {
        ActivityId = activityId;
        SegmentIndex = segmentIndex;
    }

    public string ActivityId { get; }

    public int SegmentIndex { get; }
}
