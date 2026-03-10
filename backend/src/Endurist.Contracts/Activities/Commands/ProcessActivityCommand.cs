namespace Endurist.Contracts.Activities.Commands;

public sealed class ProcessActivityCommand : CommandBase
{
    public ProcessActivityCommand(string userId, string activityId)
        : base(userId)
    {
        ActivityId = activityId;
    }

    public string ActivityId { get; set; }
}
