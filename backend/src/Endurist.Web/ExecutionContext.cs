using System.Security.Claims;

namespace Endurist.Web;

public sealed class ExecutionContext
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ExecutionContext(IHttpContextAccessor accessor)
    {
        _contextAccessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    }

    public string UserId => _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
