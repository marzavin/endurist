using Endurist.Models.Widgets;

namespace Endurist.Contracts.Widgets.Queries;

public class GetProfileWidgetQuery : QueryBase<QueryReply<WidgetModel>>
{
    public GetProfileWidgetQuery(string profileId, string widgetId)
    {
        ProfileId = profileId;
        WidgetId = widgetId;
    }

    public string ProfileId { get; }

    public string WidgetId { get; }
}
