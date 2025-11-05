namespace Endurist.Worker.Tasks;

internal abstract class BackgroundTaskBase<TInput>
    where TInput : TaskInputBase
{
    public abstract Task ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}
