using Endurist.Contracts;
using Endurist.Contracts.Activities.Queries;
using Endurist.Contracts.Exceptions;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using Endurist.Models.Activities;
using Endurist.Reader.Mappers.Activities;
using SideEffect.Messaging.RPC;

namespace Endurist.Reader.Handlers.Activities;

internal sealed class GetActivityQueryHandler : RequestHandlerBase<GetActivityQuery, QueryReply<ActivityModel>>
{
    private readonly Storage _storage;

    public GetActivityQueryHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task<QueryReply<ActivityModel>> HandleAsync(GetActivityQuery message, CancellationToken cancellationToken = default)
    {
        var activity = await _storage.Activities.GetByIdAsync(message.ActivityId, cancellationToken)
            ?? throw new EntityNotFoundException(typeof(ActivityDocument), message.ActivityId);

        var data = new ActivityModel();
        ActivityMapper.Map(activity, data);

        return new QueryReply<ActivityModel> { Data = data };
    }
}
