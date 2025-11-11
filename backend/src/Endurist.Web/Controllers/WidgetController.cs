using Endurist.Common.Models;
using Endurist.Core.Widgets.Models;
using Endurist.Core.Widgets.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endurist.Web.Controllers;

/// <summary>
/// API controller to handle widget requests.
/// </summary>
[ApiController]
[Authorize]
[Produces("application/json")]
public class WidgetController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes new instance of <see cref="WidgetController"/>.
    /// </summary>
    /// <param name="mediator">See <see cref="IMediator"/> for more information.</param>
    /// <exception cref="ArgumentNullException">Throws an exception in case of any parameter is null.</exception>
    public WidgetController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// GET request to retrieve the single profile widget by identifier.
    /// </summary>
    /// <param name="profileId">Identifier of the profile.</param>
    /// <param name="widgetId">Identifier of the widget.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="WidgetModel"/> for more information.</returns>
    [HttpGet("api/profiles/{profileId:mongoId}/widgets/{widgetId:mongoId}")]
    [ProducesResponseType<DataResponse<WidgetModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfileWidgetAsync(
        [FromRoute] string profileId,
        [FromRoute] string widgetId,
        CancellationToken cancellationToken = default)
    {
        var request = new GetProfileWidgetRequest(profileId, widgetId);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// GET request to retrieve the single activity widget by identifier.
    /// </summary>
    /// <param name="activityId">Identifier of the activity.</param>
    /// <param name="widgetId">Identifier of the widget.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="WidgetModel"/> for more information.</returns>
    [HttpGet("api/activities/{activityId:mongoId}/widgets/{widgetId:mongoId}")]
    [ProducesResponseType<DataResponse<WidgetModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivityWidgetAsync(
        [FromRoute] string activityId,
        [FromRoute] string widgetId,
        CancellationToken cancellationToken = default)
    {
        var request = new GetActivityWidgetRequest(activityId, widgetId);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
