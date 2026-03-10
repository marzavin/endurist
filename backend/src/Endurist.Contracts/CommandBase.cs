using SideEffect.Messaging.PubSub;

namespace Endurist.Contracts;

public abstract class CommandBase : IEvent 
{
    protected CommandBase(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}
