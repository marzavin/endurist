using Endurist.Contracts;
using Endurist.Contracts.Widgets.Queries;
using Endurist.Core.Widgets;
using Endurist.Data;
using Endurist.Models.Widgets;

namespace Endurist.Reader.Handlers.Widgets;

internal sealed class GetProfileWidgetQueryHandler(Storage storage, IEnumerable<WidgetBase> widgets) 
    : WidgetQueryHandlerBase<GetProfileWidgetQuery, QueryReply<WidgetModel>>(storage, widgets)
{
    public override async Task<QueryReply<WidgetModel>> HandleAsync(GetProfileWidgetQuery message, CancellationToken cancellationToken = default)
    {
        var settings = new WidgetSettingsModel { ProfilleId = message.ProfileId };
        return await GetWidgetAsync(message.WidgetId, settings, cancellationToken);
    }
}
