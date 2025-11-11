using Endurist.Common.Models;
using Endurist.Core.Widgets.Models;
using MediatR;

namespace Endurist.Core.Widgets.Requests;

public class GetActivityWidgetRequest : IRequest<DataResponse<WidgetModel>>
{
    public GetActivityWidgetRequest(string activityId, string widgetId)
    {
        ActivityId = activityId;
        WidgetId = widgetId;
    }

    public string ActivityId { get; }

    public string WidgetId { get; }
}
