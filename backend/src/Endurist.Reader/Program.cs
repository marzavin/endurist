using Endurist.Contracts;
using Endurist.Contracts.Activities.Queries;
using Endurist.Contracts.Files.Queries;
using Endurist.Contracts.Profiles.Queries;
using Endurist.Core.Widgets;
using Endurist.Models.Activities;
using Endurist.Models.Files;
using Endurist.Models.Profiles;
using Endurist.Reader.Handlers.Activities;
using Endurist.Reader.Handlers.Files;
using Endurist.Reader.Handlers.Profiles;
using Endurist.Reader.Registrations;
using Endurist.ServiceDefaults;
using SideEffect.Messaging.RabbitMQ;
using System.Reflection;

Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults("Endurist Reader Service");

var rabbitConnection = builder.Configuration.GetConnectionString("rabbitmq");
var settings = new MessageHubSettings { ConnectionString = rabbitConnection };

builder.Services.AddScoped<WidgetBase, TrainingVolumeWidget>();

builder.Services.AddRabbitMQMessageHub(settings, (options) =>
{
    options.Registry.AddRemoteProcedureCallHandler<GetActivitiesQuery, QueryPageReply<ActivityPreviewModel>, GetActivitiesQueryHandler>();
    options.Registry.AddRemoteProcedureCallHandler<GetActivityQuery, QueryReply<ActivityModel>, GetActivityQueryHandler>();
    options.Registry.AddRemoteProcedureCallHandler<GetSegmentQuery, QueryReply<SegmentModel>, GetSegmentQueryHandler>();
    options.Registry.AddRemoteProcedureCallHandler<GetFilesQuery, QueryPageReply<FilePreviewModel>, GetFilesQueryHandler>();
    options.Registry.AddRemoteProcedureCallHandler<DownloadFileQuery, QueryReply<FileDownloadModel>, DownloadFileQueryHandler>();
    options.Registry.AddRemoteProcedureCallHandler<GetProfilesQuery, QueryPageReply<ProfilePreviewModel>, GetProfilesQueryHandler>();
    options.Registry.AddRemoteProcedureCallHandler<GetProfileQuery, QueryReply<ProfileModel>, GetProfileQueryHandler>();
});

builder.AddMongoStorage();

var app = builder.Build();

app.MapDefaultEndpoints();

await app.RunAsync();
