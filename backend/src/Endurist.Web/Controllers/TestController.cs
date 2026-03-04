using Endurist.Contracts.Commands;
using Microsoft.AspNetCore.Mvc;
using SideEffect.Messaging;

namespace Endurist.Web.Controllers;

[ApiController]
[Route("api/tests")]
[Produces("application/json")]
public class TestController : ControllerBase
{
    private readonly IMessageHubClient _hubClient;

    public TestController(IMessageHubClient hubClient)
    {
        _hubClient = hubClient ?? throw new ArgumentNullException(nameof(hubClient));
    }

    [HttpPost]
    public async Task<IActionResult> TriggerFileUploadAsync(CancellationToken cancellationToken = default)
    {
        var eventMessage = new UploadFileCommand { Path = "", UserId = "" };
        await _hubClient.PublishEventAsync(eventMessage, cancellationToken);

        return NoContent();
    }
}
