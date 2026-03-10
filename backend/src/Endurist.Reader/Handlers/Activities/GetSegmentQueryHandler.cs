using Endurist.Contracts;
using Endurist.Contracts.Activities.Queries;
using Endurist.Data;
using Endurist.Models.Activities;
using Endurist.Reader.Mappers.Activities;
using SideEffect.Messaging.RPC;

namespace Endurist.Reader.Handlers.Activities;

internal sealed class GetSegmentQueryHandler : RequestHandlerBase<GetSegmentQuery, QueryReply<SegmentModel>>
{
    private readonly Storage _storage;

    public GetSegmentQueryHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task<QueryReply<SegmentModel>> HandleAsync(GetSegmentQuery message, CancellationToken cancellationToken = default)
    {
        var activity = await _storage.Activities.GetByIdAsync(message.ActivityId, cancellationToken);
        var segment = activity.Segments.OrderBy(x => x.StartIndex).Skip(message.SegmentIndex - 1).First();

        var data = new SegmentModel();
        SegmentMapper.Map(segment, data, activity.Track);

        return new QueryReply<SegmentModel> { Data = data };
    }
}
