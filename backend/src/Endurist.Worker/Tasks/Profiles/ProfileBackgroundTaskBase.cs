using System.Text.Json;

namespace Endurist.Worker.Tasks.Profiles;

internal abstract class ProfileBackgroundTaskBase<TInput> : BackgroundTaskBase<TInput>
    where TInput : ProfileTaskInputBase
{
    protected string SerializeData(object data)
    {
        return JsonSerializer.Serialize(data);
    }
}
