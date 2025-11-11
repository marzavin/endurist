using Endurist.Common.Models;
using Endurist.Core.Widgets.Features;
using Endurist.Core.Widgets.Models;
using Endurist.Core.Widgets.Requests;
using Endurist.Data;
using MediatR;

namespace Endurist.Core.Widgets;

public class WidgetHandler : HandlerBase,
    IRequestHandler<GetProfileWidgetRequest, DataResponse<WidgetModel>>,
    IRequestHandler<GetActivityWidgetRequest, DataResponse<WidgetModel>>
{
    protected IEnumerable<WidgetBase> Widgets { get; }

    public WidgetHandler(Storage storage, IEnumerable<WidgetBase> widgets)
        : base(storage)
    {
        Widgets = widgets;
    }

    public async Task<DataResponse<WidgetModel>> Handle(GetProfileWidgetRequest request, CancellationToken cancellationToken)
    {
        var settings = new WidgetSettingsModel { ProfilleId = request.ProfileId };
        return await GetWidgetAsync(request.WidgetId, settings, cancellationToken);
    }

    public async Task<DataResponse<WidgetModel>> Handle(GetActivityWidgetRequest request, CancellationToken cancellationToken)
    {
        var settings = new WidgetSettingsModel { ActivityId = request.ActivityId };
        return await GetWidgetAsync(request.WidgetId, settings, cancellationToken);
    }

    private async Task<DataResponse<WidgetModel>> GetWidgetAsync(
        string widgetId,
        WidgetSettingsModel settings,
        CancellationToken cancellationToken = default)
    {
        var document = await Storage.Widgets.GetByIdAsync(widgetId, cancellationToken);
        var widget = Widgets?.FirstOrDefault(x => x.Id == widgetId);
        if (document is null || widget is null)
        {
            return new DataResponse<WidgetModel> { Data = null };
        }

        var widgetModel = await widget.BuildAsync(document.Name, Storage, settings, cancellationToken);

        return new DataResponse<WidgetModel> { Data = widgetModel };
    }
}
