using Endurist.Contracts;
using Endurist.Core.Widgets;
using Endurist.Data;
using Endurist.Models.Widgets;
using SideEffect.Messaging.RPC;

namespace Endurist.Reader.Handlers.Widgets;

internal abstract class WidgetQueryHandlerBase<TRequest, TResponse> : RequestHandlerBase<TRequest, TResponse>
    where TRequest : IRequest<TResponse> 
    where TResponse : IResponse, new()
{
    protected Storage Storage { get; }

    protected IEnumerable<WidgetBase> Widgets { get; }

    protected WidgetQueryHandlerBase(Storage storage, IEnumerable<WidgetBase> widgets)
    {
        Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        Widgets = widgets ?? [];
    }

    protected async Task<QueryReply<WidgetModel>> GetWidgetAsync(
        string widgetId,
        WidgetSettingsModel settings,
        CancellationToken cancellationToken = default)
    {
        var document = await Storage.Widgets.GetByIdAsync(widgetId, cancellationToken);
        var widget = Widgets?.FirstOrDefault(x => x.Id == widgetId);
        if (document is null || widget is null)
        {
            return new QueryReply<WidgetModel> { Data = null };
        }

        var widgetModel = await widget.BuildAsync(document.Name, Storage, settings, cancellationToken);

        return new QueryReply<WidgetModel> { Data = widgetModel };
    }
}
