using SideEffect.Messaging.RPC;

namespace Endurist.Contracts;

public abstract class QueryBase<TResponse> : IRequest<TResponse>
    where TResponse : ResponseBase
{
    public string UserId { get; set; }
}
