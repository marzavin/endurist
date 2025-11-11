using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Endurist.Core.Services;

public sealed class ExecutionContext
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ExecutionContext(IHttpContextAccessor accessor)
    {
        _contextAccessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    }

    public string UserId => _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
