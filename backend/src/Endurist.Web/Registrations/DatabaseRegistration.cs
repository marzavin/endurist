using Endurist.Data;
using Endurist.Data.Mongo.Repositories;
using Endurist.Hosting.Settings;

namespace Endurist.Web.Registrations;

/// <summary>
/// Configuration for application storage.
/// </summary>
internal static class DatabaseRegistration
{
    /// <summary>
    /// Extension method to add mongodb database as an application storage.
    /// </summary>
    /// <param name="services">See <see cref="IServiceCollection"/> for more information.</param>
    /// <param name="configuration">See <see cref="IConfiguration"/> for more information.</param>
    public static void AddMongoStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguration<MongoStorageConfiguration>(configuration, "MongoStorage");

        services.AddScoped<AccountRepository>();
        services.AddScoped<ActivityRepository>();
        services.AddScoped<FileRepository>();
        services.AddScoped<ProfileRepository>();
        services.AddScoped<ProfileWidgetRepository>();
        services.AddScoped<TokenRepository>();
        services.AddScoped<WidgetRepository>();

        services.AddScoped<Storage>();
    }
}
