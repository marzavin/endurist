using Endurist.Common.Models;
using Endurist.Core.Activities.Models;
using Endurist.Core.Activities.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Web.Controllers;

/// <summary>
/// API controller to handle activity requests.
/// </summary>
[ApiController]
[Authorize]
[Route("api/activities")]
[Produces("application/json")]
public class ActivityController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes new instance of <see cref="ActivityController"/>.
    /// </summary>
    /// <param name="mediator">See <see cref="IMediator"/> for more information.</param>
    /// <exception cref="ArgumentNullException">Throws an exception in case of any parameter is null.</exception>
    public ActivityController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// GET request to retrieve the list of activities.
    /// </summary>
    /// <param name="paging">See <see cref="PagingInfo"/> for more information.</param>
    /// <param name="sorting">See <see cref="SortingInfo"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="ActivityPreviewModel"/> for more information.</returns>
    [HttpGet]
    [ProducesResponseType<DataPageResponse<ActivityPreviewModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivitiesAsync(
        [FromQuery] PagingInfo paging,
        [FromQuery] SortingInfo sorting,
        CancellationToken cancellationToken = default)
    {
        var request = new GetActivitiesRequest(paging, sorting);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// GET request to retrieve the single activity by identifier.
    /// </summary>
    /// <param name="activityId">Identifier of the activity.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="ActivityModel"/> for more information.</returns>
    [HttpGet("{activityId:mongoId}")]
    [ProducesResponseType<DataResponse<ActivityModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivityAsync(
        [FromRoute] string activityId,
        CancellationToken cancellationToken = default)
    {
        var request = new GetActivityRequest(activityId);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// GET request to retrieve the single segment of activity by index.
    /// </summary>
    /// <param name="activityId">Identifier of the activity.</param>
    /// <param name="segmentIndex">Index of the segment.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="SegmentModel"/> for more information.</returns>
    [HttpGet("{activityId:mongoId}/segments/{segmentIndex:index}")]
    [ProducesResponseType<DataResponse<SegmentModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSegmentAsync(
        [FromRoute] string activityId,
        [FromRoute] int segmentIndex,
        CancellationToken cancellationToken = default)
    {
        var request = new GetSegmentRequest(activityId, segmentIndex);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
