using SideEffect.Messaging.RPC;

namespace Endurist.Contracts;

public abstract class QueryBase<TResponse> : IRequest<TResponse>
    where TResponse : ResponseBase
{
    protected QueryBase(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}
