using Endurist.Contracts.Activities.Commands;
using Endurist.Contracts.Files.Commands;
using Endurist.Contracts.Profiles.Commands;
using Endurist.Core.Services;
using Endurist.Core.Widgets;
using Endurist.ServiceDefaults;
using Endurist.Writer.Handlers;
using Endurist.Writer.Registrations;
using SideEffect.Messaging.RabbitMQ;
using System.Reflection;

Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults("Endurist Writer Service");

var rabbitConnection = builder.Configuration.GetConnectionString("rabbitmq");
var settings = new MessageHubSettings { ConnectionString = rabbitConnection };

builder.Services.AddScoped<WidgetBase, TrainingVolumeWidget>();

builder.Services.AddRabbitMQMessageHub(settings, (options) =>
{
    options.Registry.AddPublishSubscribeHandler<UploadFileCommand, UploadFileCommandHandler>();
    options.Registry.AddPublishSubscribeHandler<ProcessFileCommand, ProcessFileCommandHandler>();
    options.Registry.AddPublishSubscribeHandler<ProcessActivityCommand, ProcessActivityCommandHandler>();
    options.Registry.AddPublishSubscribeHandler<ProcessProfileCommand, ProcessProfileCommandHandler>();
});

builder.Services.AddSingleton<IEncryptionService, EncryptionService>();

builder.AddMongoStorage();

var app = builder.Build();

app.MapDefaultEndpoints();

await app.RunAsync();