using Endurist.Web.Constraints;

namespace Endurist.Web.Registrations;

/// <summary>
/// Configuration for routing engine.
/// </summary>
internal static class ConstraintRegistration
{
    /// <summary>
    /// Extension method to add custom route param constraint.
    /// </summary>
    /// <param name="services">See <see cref="IServiceCollection"/> for more information.</param>
    internal static void AddRouteConstraints(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(routeOptions => { routeOptions.ConstraintMap.Add("mongoId", typeof(MongoIdentifierConstraint)); });
        services.Configure<RouteOptions>(routeOptions => { routeOptions.ConstraintMap.Add("index", typeof(ArrayIndexConstraint)); });
    }
}
