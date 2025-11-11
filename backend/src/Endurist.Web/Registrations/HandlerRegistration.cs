using Endurist.Core;

namespace Endurist.Web.Registrations;

/// <summary>
/// Configuration for business logic handlers.
/// </summary>
internal static class HandlerRegistration
{
    /// <summary>
    /// Extension method to add business logic handlers.
    /// </summary>
    /// <param name="services">See <see cref="IServiceCollection"/> for more information.</param>
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(HandlerBase).Assembly));
    }
}
