using Endurist.Core.Services;
using Endurist.Data;
using Endurist.Data.Mongo.Repositories;
using Endurist.Hosting.Settings;
using SideEffect.Messaging;
using SideEffect.Messaging.Redis;
using RedisStorageConfiguration = Endurist.Hosting.Settings.RedisStorageConfiguration;

namespace Endurist.Worker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddConfiguration<MongoStorageConfiguration>(configuration, "MongoStorage");
        services.AddConfiguration<FileStorageConfiguration>(configuration, "FileStorage");

        //TODO:AMZ: == SideEffect.Messaging
        services.AddConfiguration<RedisStorageConfiguration>(configuration, "RedisStorage");

        services.AddSingleton<IEncryptionService, EncryptionService>();

        services.AddSingleton<IServiceBus, RedisServiceBus>();

        services.AddTransient<AccountRepository>();
        services.AddTransient<ActivityRepository>();
        services.AddTransient<FileRepository>();
        services.AddTransient<ProfileRepository>();
        services.AddTransient<ProfileWidgetRepository>();
        services.AddTransient<TokenRepository>();
        services.AddTransient<WidgetRepository>();
        services.AddTransient<Storage>();

        services.AddHostedService<FileProcessingWorker>();
        services.AddHostedService<FileParsingWorker>();

        var host = builder.Build();
        host.Run();
    }
}
