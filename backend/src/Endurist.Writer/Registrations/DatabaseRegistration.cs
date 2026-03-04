using Endurist.Data;
using Endurist.Data.Mongo.Repositories;
using Endurist.Data.Mongo.Settings;

namespace Endurist.Writer.Registrations;

/// <summary>
/// Configuration for application storage.
/// </summary>
public static class StorageRegistration
{
    /// <summary>
    /// Extension method to add mongodb database as an application storage.
    /// </summary>
    /// <param name="services">See <see cref="IServiceCollection"/> for more information.</param>
    /// <param name="configuration">See <see cref="IConfiguration"/> for more information.</param>
    public static void AddMongoStorage(this WebApplicationBuilder builder)
    {
        var mongoConnection = builder.Configuration.GetConnectionString("mongodb");
        var settings = new StorageConfiguration { ConnectionString = mongoConnection };

        builder.Services.AddSingleton(x => settings);

        builder.Services.AddScoped<ActivityRepository>();
        builder.Services.AddScoped<FileRepository>();
        builder.Services.AddScoped<ProfileRepository>();
        builder.Services.AddScoped<ProfileWidgetRepository>();
        builder.Services.AddScoped<WidgetRepository>();

        builder.Services.AddScoped<Storage>();
    }
}
