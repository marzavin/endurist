using Endurist.Core.Accounts.Models;
using Endurist.Core.Accounts.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endurist.Web.Controllers;

/// <summary>
/// API controller to handle user accounts.
/// </summary>
[ApiController]
[Route("api/tokens")]
[Produces("application/json")]
public class TokenController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes new instance of <see cref="TokenController"/>.
    /// </summary>
    /// <param name="mediator">See <see cref="IMediator"/> for more information.</param>
    /// <exception cref="ArgumentNullException">Throws an exception in case of any parameter is null.</exception>
    public TokenController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// POST request to create token for user.
    /// </summary>
    /// <param name="model">See <see cref="CredentialsModel"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="TokenModel"/> for more information.</returns>
    [HttpPost("create")]
    [AllowAnonymous]
    [ProducesResponseType<TokenModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Token([FromBody]CredentialsModel model, CancellationToken cancellationToken)
    {
        var request = new CreateTokenRequest(model.Name, model.Password);
        var response = await _mediator.Send(request, cancellationToken);
        if (response?.Data is null)
        {
            return BadRequest("Invalid email or password.");
        }

        return Ok(response.Data);
    }

    /// <summary>
    /// POST request to refresh token for user.
    /// </summary>
    /// <param name="model">See <see cref="CredentialsModel"/> for more information.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/> for more information.</param>
    /// <returns>See <see cref="TokenModel"/> for more information.</returns>
    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType<TokenModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model, CancellationToken cancellationToken)
    {
        var request = new RefreshTokenRequest(model.RefreshToken);
        var response = await _mediator.Send(request, cancellationToken);
        if (response?.Data is null)
        {
            return BadRequest("Invalid refresh token.");
        }

        return Ok(response.Data);
    }
}