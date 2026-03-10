using Endurist.Contracts.Activities.Commands;
using Endurist.Contracts.Exceptions;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using SideEffect.Messaging.PubSub;

namespace Endurist.Writer.Handlers;

internal class ProcessActivityCommandHandler : EventHandlerBase<ProcessActivityCommand>
{
    private readonly Storage _storage;

    public ProcessActivityCommandHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task HandleAsync(ProcessActivityCommand message, CancellationToken cancellationToken = default)
    {
        var activity = await _storage.Activities.GetByIdAsync(message.ActivityId, cancellationToken)
            ?? throw new EntityNotFoundException(typeof(ActivityDocument), message.ActivityId);

        //await _storage.Activities.UpdateAsync(activity, cancellationToken);
    }
}
