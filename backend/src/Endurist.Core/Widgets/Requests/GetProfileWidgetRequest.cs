using Endurist.Common.Models;
using Endurist.Core.Widgets.Models;
using MediatR;

namespace Endurist.Core.Widgets.Requests;

public class GetProfileWidgetRequest : IRequest<DataResponse<WidgetModel>>
{
    public GetProfileWidgetRequest(string profileId, string widgetId)
    {
        ProfileId = profileId;
        WidgetId = widgetId;
    }

    public string ProfileId { get; }

    public string WidgetId { get; }
}
