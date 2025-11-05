using Endurist.Data;
using SideEffect.Messaging;

namespace Endurist.Worker.Tasks.Profiles.TrainingVolume;

internal class TrainingVolumeCalculationWorker : IHostedService
{
    private readonly IServiceBus _serviceBus;
    private readonly Storage _storage;

    public TrainingVolumeCalculationWorker(IServiceBus serviceBus, Storage storage)
    {
        _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _serviceBus.SubscribeToEventAsync<TrainingVolumeCalculationInput>(Calculate, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _serviceBus.UnsubscribeFromEventAsync<TrainingVolumeCalculationInput>(cancellationToken: cancellationToken);
    }

    private async Task Calculate(TrainingVolumeCalculationInput input, CancellationToken cancellationToken)
    {
        var task = new TrainingVolumeCalculationTask(_storage);
        await task.ExecuteAsync(input, cancellationToken);
    }
}
