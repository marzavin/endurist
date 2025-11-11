using Endurist.Common.Models;
using Endurist.Core.Profiles.Models;
using Endurist.Core.Profiles.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SideEffect.DataTransfer.Paging;
using SideEffect.DataTransfer.Sorting;

namespace Endurist.Web.Controllers;

/// <summary>
/// API controller to handle profile requests.
/// </summary>
[ApiController]
[Authorize]
[Route("api/profiles")]
[Produces("application/json")]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes new instance of <see cref="ProfileController"/>.
    /// </summary>
    /// <param name="mediator">See <see cref="IMediator"/> for more information.</param>
    /// <exception cref="ArgumentNullException">Throws an exception in case of any parameter is null.</exception>
    public ProfileController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// GET request to retrieve the list of profiles.
    /// </summary>
    /// <param name="paging">See <see cref="Paging"/> for more information.</param>
    /// <param name="sorting">See <see cref="SortingInfo"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="ProfilePreviewModel"/> for more information.</returns>
    [HttpGet]
    [ProducesResponseType<DataPageResponse<ProfilePreviewModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfilesAsync(
        [FromQuery] PagingInfo paging,
        [FromQuery] SortingInfo sorting,
        CancellationToken cancellationToken = default)
    {
        var request = new GetProfilesRequest(paging, sorting);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// GET request to retrieve the single profile by identifier.
    /// </summary>
    /// <param name="profileId">Identifier of the profile.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="ProfileModel"/> for more information.</returns>
    [HttpGet("{profileId:mongoId}")]
    [ProducesResponseType<DataResponse<ProfileModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfileAsync([FromRoute] string profileId, CancellationToken cancellationToken = default)
    {
        var request = new GetProfileRequest(profileId);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}