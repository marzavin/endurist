using Endurist.Contracts;
using Endurist.Contracts.Widgets.Queries;
using Endurist.Models.Widgets;
using Microsoft.AspNetCore.Mvc;
using SideEffect.Messaging;

namespace Endurist.Web.Controllers;

/// <summary>
/// API controller to handle widget requests.
/// </summary>
[ApiController]
[Produces("application/json")]
public class WidgetController(ExecutionContext executionContext, IMessageHubClient hub) 
    : MessageHubControllerBase(executionContext, hub)
{
    /// <summary>
    /// GET request to retrieve the single profile widget by identifier.
    /// </summary>
    /// <param name="profileId">Identifier of the profile.</param>
    /// <param name="widgetId">Identifier of the widget.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="WidgetModel"/> for more information.</returns>
    [HttpGet("api/profiles/{profileId:mongoId}/widgets/{widgetId:mongoId}")]
    [ProducesResponseType<QueryReply<WidgetModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfileWidgetAsync(
        [FromRoute] string profileId,
        [FromRoute] string widgetId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetProfileWidgetQuery(ExecutionContext.UserId, profileId, widgetId);
        var reply = await Hub.ExecuteRequestAsync<GetProfileWidgetQuery, QueryReply<WidgetModel>>(query, cancellationToken);
        return Ok(reply);
    }

    /// <summary>
    /// GET request to retrieve the single activity widget by identifier.
    /// </summary>
    /// <param name="activityId">Identifier of the activity.</param>
    /// <param name="widgetId">Identifier of the widget.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="WidgetModel"/> for more information.</returns>
    [HttpGet("api/activities/{activityId:mongoId}/widgets/{widgetId:mongoId}")]
    [ProducesResponseType<QueryReply<WidgetModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivityWidgetAsync(
        [FromRoute] string activityId,
        [FromRoute] string widgetId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetActivityWidgetQuery(ExecutionContext.UserId, activityId, widgetId);
        var reply = await Hub.ExecuteRequestAsync<GetActivityWidgetQuery, QueryReply<WidgetModel>>(query, cancellationToken);
        return Ok(reply);
    }
}
