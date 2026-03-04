using SideEffect.Messaging.PubSub;

namespace Endurist.Contracts.Commands;

public abstract class CommandBase : IEvent 
{
    public string UserId { get; set; }
}
