using Endurist.Contracts;
using Endurist.Contracts.Activities.Queries;
using Endurist.Models.Activities;
using Microsoft.AspNetCore.Mvc;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;
using SideEffect.Messaging;

namespace Endurist.Web.Controllers;

/// <summary>
/// API controller to handle activity requests.
/// </summary>
[ApiController]
[Route("api/activities")]
[Produces("application/json")]
public class ActivityController(ExecutionContext executionContext, IMessageHubClient hub) 
    : MessageHubControllerBase(executionContext, hub)
{
    /// <summary>
    /// GET request to retrieve the list of activities.
    /// </summary>
    /// <param name="paging">See <see cref="PagingInfo"/> for more information.</param>
    /// <param name="sorting">See <see cref="SortingInfo"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="ActivityPreviewModel"/> for more information.</returns>
    [HttpGet]
    [ProducesResponseType<QueryPageReply<ActivityPreviewModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivitiesAsync(
        [FromQuery] PagingInfo paging,
        [FromQuery] SortingInfo sorting,
        CancellationToken cancellationToken = default)
    {
        var query = new GetActivitiesQuery(paging, sorting);
        var reply = await Hub.ExecuteRequestAsync<GetActivitiesQuery, QueryPageReply<ActivityPreviewModel>>(query, cancellationToken);
        return Ok(reply);
    }

    /// <summary>
    /// GET request to retrieve the single activity by identifier.
    /// </summary>
    /// <param name="activityId">Identifier of the activity.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="ActivityModel"/> for more information.</returns>
    [HttpGet("{activityId:mongoId}")]
    [ProducesResponseType<QueryReply<ActivityModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActivityAsync(
        [FromRoute] string activityId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetActivityQuery(activityId);
        var reply = await Hub.ExecuteRequestAsync<GetActivityQuery, QueryReply<ActivityModel>>(query, cancellationToken);
        return Ok(reply);
    }

    /// <summary>
    /// GET request to retrieve the single segment of activity by index.
    /// </summary>
    /// <param name="activityId">Identifier of the activity.</param>
    /// <param name="segmentIndex">Index of the segment.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="SegmentModel"/> for more information.</returns>
    [HttpGet("{activityId:mongoId}/segments/{segmentIndex:index}")]
    [ProducesResponseType<QueryReply<SegmentModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSegmentAsync(
        [FromRoute] string activityId,
        [FromRoute] int segmentIndex,
        CancellationToken cancellationToken = default)
    {
        var query = new GetSegmentQuery(activityId, segmentIndex);
        var reply = await Hub.ExecuteRequestAsync<GetSegmentQuery, QueryReply<SegmentModel>>(query, cancellationToken);
        return Ok(reply);
    }
}
