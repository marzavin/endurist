using Endurist.Common.Exceptions;
using Endurist.Contracts.Commands;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using SideEffect.Messaging.PubSub;

namespace Endurist.Worker.Handlers;

internal class ProcessActivityCommandHandler : EventHandlerBase<ProcessActivityCommand>
{
    private readonly Storage _storage;

    public ProcessActivityCommandHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task HandleAsync(ProcessActivityCommand message, CancellationToken cancellationToken = default)
    {
        var activity = await _storage.Activities.GetByIdAsync(message.Id, cancellationToken)
            ?? throw new EntityNotFoundException(typeof(ActivityDocument), message.Id);

        //await _storage.Activities.UpdateAsync(activity, cancellationToken);
    }
}
