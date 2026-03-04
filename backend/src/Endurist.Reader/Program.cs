using Endurist.Contracts.Queries;
using Endurist.Reader.Handlers;
using Endurist.Reader.Registrations;
using Endurist.ServiceDefaults;
using SideEffect.Messaging.RabbitMQ;
using System.Reflection;

Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults("Endurist Reader Service");

var rabbitConnection = builder.Configuration.GetConnectionString("rabbitmq");
var settings = new MessageHubSettings { ConnectionString = rabbitConnection };

builder.Services.AddRabbitMQMessageHub(settings, (options) =>
{
    options.Registry.AddRemoteProcedureCallHandler<GetActivitiesRequest, GetActivitiesResponse, GetActivitiesQueryHandler>();
});

builder.AddMongoStorage();

var app = builder.Build();

app.MapDefaultEndpoints();

await app.RunAsync();
