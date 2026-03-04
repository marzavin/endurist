using Endurist.Contracts.Queries;
using SideEffect.Messaging.RPC;

namespace Endurist.Reader.Handlers;

internal sealed class GetActivitiesQueryHandler : RequestHandlerBase<GetActivitiesRequest, GetActivitiesResponse>
{
    public override Task<GetActivitiesResponse> HandleAsync(GetActivitiesRequest message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
