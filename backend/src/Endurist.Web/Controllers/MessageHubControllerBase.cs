using Microsoft.AspNetCore.Mvc;
using SideEffect.Messaging;

namespace Endurist.Web.Controllers;

[ApiController]
public abstract class MessageHubControllerBase(ExecutionContext executionContext, IMessageHubClient hub) : ControllerBase
{
    protected ExecutionContext ExecutionContext { get; } = executionContext ?? throw new ArgumentNullException(nameof(executionContext));

    protected IMessageHubClient Hub { get; } = hub ?? throw new ArgumentNullException(nameof(hub));
}
