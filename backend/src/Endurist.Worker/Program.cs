using Endurist.Contracts.Commands;
using Endurist.Core.Services;
using Endurist.Hosting.Settings;
using Endurist.ServiceDefaults;
using Endurist.Worker.Handlers;
using Endurist.Worker.Registrations;
using SideEffect.Messaging.RabbitMQ;
using System.Reflection;

namespace Endurist.Worker;

public class Program
{
    public static async Task Main(string[] args)
    {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults("Endurist Worker Service");

        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddConfiguration<FileStorageConfiguration>(configuration, "FileStorage");

        var rabbitConnection = builder.Configuration.GetConnectionString("rabbitmq");
        var settings = new MessageHubSettings { ConnectionString = rabbitConnection };

        builder.Services.AddRabbitMQMessageHub(settings, (options) =>
        {
            options.Registry.AddPublishSubscribeHandler<UploadFileCommand, UploadFileCommandHandler>();
            options.Registry.AddPublishSubscribeHandler<ProcessFileCommand, ProcessFileCommandHandler>();
            options.Registry.AddPublishSubscribeHandler<ProcessActivityCommand, ProcessActivityCommandHandler>();
            options.Registry.AddPublishSubscribeHandler<ProcessProfileCommand, ProcessProfileCommandHandler>();
        });

        services.AddSingleton<IEncryptionService, EncryptionService>();

        builder.AddMongoStorage();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        await app.RunAsync();
    }
}
