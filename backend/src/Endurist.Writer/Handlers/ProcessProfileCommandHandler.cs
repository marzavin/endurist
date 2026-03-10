using Endurist.Contracts.Exceptions;
using Endurist.Contracts.Profiles.Commands;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using SideEffect.Messaging.PubSub;

namespace Endurist.Writer.Handlers;

internal class ProcessProfileCommandHandler : EventHandlerBase<ProcessProfileCommand>
{
    private readonly Storage _storage;

    public ProcessProfileCommandHandler(Storage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public override async Task HandleAsync(ProcessProfileCommand message, CancellationToken cancellationToken = default)
    {
        var profile = await _storage.Profiles.GetByIdAsync(message.ProfileId, cancellationToken)
            ?? throw new EntityNotFoundException(typeof(ProfileDocument), message.ProfileId);

        //await _storage.Profiles.UpdateAsync(profile, cancellationToken);
    }
}
