using Endurist.Core.Services;
using Endurist.Data;
using Endurist.Data.Mongo.Repositories;
using Endurist.Hosting.Settings;
using Endurist.Worker.Tasks.Profiles.TrainingVolume;
using SideEffect.Messaging;
using SideEffect.Messaging.Redis;
using System.Reflection;

namespace Endurist.Worker;

public class Program
{
    public static void Main(string[] args)
    {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

        var builder = Host.CreateApplicationBuilder(args);

        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddConfiguration<MongoStorageConfiguration>(configuration, "MongoStorage");
        services.AddConfiguration<FileStorageConfiguration>(configuration, "FileStorage");
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

        services.AddHostedService<FileAnalysisWorker>();
        services.AddHostedService<FileUploadWorker>();
        services.AddHostedService<TrainingVolumeCalculationWorker>();

        var host = builder.Build();
        host.Run();
    }
}
