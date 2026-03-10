using Endurist.Contracts;
using Endurist.Contracts.Widgets.Queries;
using Endurist.Core.Widgets;
using Endurist.Data;
using Endurist.Models.Widgets;

namespace Endurist.Reader.Handlers.Widgets;

internal sealed class GetActivityWidgetQueryHandler(Storage storage, IEnumerable<WidgetBase> widgets) 
    : WidgetQueryHandlerBase<GetActivityWidgetQuery, QueryReply<WidgetModel>>(storage, widgets)
{
    public override async Task<QueryReply<WidgetModel>> HandleAsync(GetActivityWidgetQuery message, CancellationToken cancellationToken = default)
    {
        var settings = new WidgetSettingsModel { ActivityId = message.ActivityId };
        return await GetWidgetAsync(message.WidgetId, settings, cancellationToken);
    }
}
