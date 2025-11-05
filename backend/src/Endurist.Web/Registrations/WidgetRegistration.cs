using Endurist.Core.Widgets.Features;

namespace Endurist.Web.Registrations;

/// <summary>
/// Configuration for widgets.
/// </summary>
internal static class WidgetRegistration
{
    /// <summary>
    /// Extension method to add widgets.
    /// </summary>
    /// <param name="services">See <see cref="IServiceCollection"/> for more information.</param>
    public static void AddWidgets(this IServiceCollection services)
    {
        services.AddScoped<WidgetBase, TrainingVolumeWidget>();
    }
}
