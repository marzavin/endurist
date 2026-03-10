using Endurist.Models.Widgets;

namespace Endurist.Contracts.Widgets.Queries;

public class GetActivityWidgetQuery : QueryBase<QueryReply<WidgetModel>>
{
    public GetActivityWidgetQuery(string activityId, string widgetId)
    {
        ActivityId = activityId;
        WidgetId = widgetId;
    }

    public string ActivityId { get; }

    public string WidgetId { get; }
}
