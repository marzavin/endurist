using SideEffect.Messaging;

namespace Endurist.Core.Events;

public class TrainingVolumeCalculationEvent : IMessage
{
    public string ProfileId { get; set; }
}
