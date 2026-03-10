using SideEffect.Messaging.RPC;

namespace Endurist.Contracts;

public class QueryReply<TData> : ResponseBase
{
    public TData Data { get; set; }
}
