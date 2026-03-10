using Endurist.Contracts;
using Endurist.Contracts.Profiles.Queries;
using Endurist.Models.Profiles;
using Microsoft.AspNetCore.Mvc;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;
using SideEffect.Messaging;

namespace Endurist.Web.Controllers;

/// <summary>
/// API controller to handle profile requests.
/// </summary>
[ApiController]
[Route("api/profiles")]
[Produces("application/json")]
public class ProfileController(ExecutionContext executionContext, IMessageHubClient hub) 
    : MessageHubControllerBase(executionContext, hub)
{
    /// <summary>
    /// GET request to retrieve the list of profiles.
    /// </summary>
    /// <param name="paging">See <see cref="Paging"/> for more information.</param>
    /// <param name="sorting">See <see cref="SortingInfo"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="ProfilePreviewModel"/> for more information.</returns>
    [HttpGet]
    [ProducesResponseType<QueryPageReply<ProfilePreviewModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfilesAsync(
        [FromQuery] PagingInfo paging,
        [FromQuery] SortingInfo sorting,
        CancellationToken cancellationToken = default)
    {
        var query = new GetProfilesQuery(ExecutionContext.UserId, paging, sorting);
        var reply = await Hub.ExecuteRequestAsync<GetProfilesQuery, QueryPageReply<ProfilePreviewModel>>(query, cancellationToken);
        return Ok(reply);
    }

    /// <summary>
    /// GET request to retrieve the single profile by identifier.
    /// </summary>
    /// <param name="profileId">Identifier of the profile.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="ProfileModel"/> for more information.</returns>
    [HttpGet("{profileId:mongoId}")]
    [ProducesResponseType<QueryReply<ProfileModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfileAsync([FromRoute] string profileId, CancellationToken cancellationToken = default)
    {
        var query = new GetProfileQuery(ExecutionContext.UserId, profileId);
        var reply = await Hub.ExecuteRequestAsync<GetProfileQuery, QueryReply<ProfileModel>>(query, cancellationToken);
        return Ok(reply);
    }
}
