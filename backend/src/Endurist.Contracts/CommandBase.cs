using SideEffect.Messaging.PubSub;

namespace Endurist.Contracts;

public abstract class CommandBase : IEvent 
{
    public string UserId { get; set; }
}
