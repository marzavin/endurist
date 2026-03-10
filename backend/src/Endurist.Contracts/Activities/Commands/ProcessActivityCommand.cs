namespace Endurist.Contracts.Activities.Commands;

public sealed class ProcessActivityCommand : CommandBase
{
    public ProcessActivityCommand(string activityId)
    {
        ActivityId = activityId;
    }

    public string ActivityId { get; set; }
}
