using Endurist.Core.Widgets.Models;
using Endurist.Data;
using System.Text.Json;

namespace Endurist.Core.Widgets.Features;

public abstract class WidgetBase
{
    public abstract string Id { get; }

    public static JsonSerializerOptions SerializerOptions { get; } = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public virtual async Task<WidgetModel> BuildAsync(string name, Storage storage, WidgetSettingsModel settings, CancellationToken cancellationToken = default)
    {
        var widgetData = await GetWidgetDataAsync(storage, settings, cancellationToken);

        return new WidgetModel
        {
            Id = Id,
            Name = name,
            Data = widgetData is null ? null : SerializeData(widgetData)
        };
    }

    protected string SerializeData(object data)
    {
        return JsonSerializer.Serialize(data, SerializerOptions);
    }

    protected abstract Task<object> GetWidgetDataAsync(Storage storage, WidgetSettingsModel settings, CancellationToken cancellationToken = default);
}
