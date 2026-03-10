using Endurist.Models.Activities;

namespace Endurist.Contracts.Activities.Queries;

public class GetActivityQuery : QueryBase<QueryReply<ActivityModel>>
{
    public GetActivityQuery(string userId, string activityId)
        : base(userId)
    {
        ActivityId = activityId;
    }

    public string ActivityId { get; }
}
