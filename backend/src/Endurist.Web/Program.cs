using Endurist.Core.Services;
using Endurist.ServiceDefaults;
using Endurist.Web.Middlewares;
using Endurist.Web.Registrations;
using Microsoft.AspNetCore.HttpOverrides;
using SideEffect.Messaging.RabbitMQ;
using System.Reflection;

const string AllowedOriginsPolicy = "AllowedOrigins";

Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults("Endurist Web Gateway");

var services = builder.Services;
var configuration = builder.Configuration;

services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.TryAdd(GlobalExceptionHandler.RequestIdKey, context.HttpContext.TraceIdentifier);
    };
});
services.AddExceptionHandler<GlobalExceptionHandler>();

services.AddCors(options =>
{
    options.AddPolicy(name: AllowedOriginsPolicy, policy  => { policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); });
});

services.AddRouteConstraints();

services.AddAuthorization();

services.AddSingleton<IEncryptionService, EncryptionService>();

var rabbitConnection = builder.Configuration.GetConnectionString("rabbitmq");
var settings = new MessageHubSettings { ConnectionString = rabbitConnection };

builder.Services.AddRabbitMQMessageHub(settings);

services.AddHttpContextAccessor();
services.AddScoped<Endurist.Core.Services.ExecutionContext>();

services.AddWidgets();

services.AddControllers();

services.AddEndpointsApiExplorer();

services.Configure<ForwardedHeadersOptions>(options => {
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseForwardedHeaders();

app.UseCors(AllowedOriginsPolicy);

app.MapDefaultEndpoints();

app.MapOpenApi();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

await app.RunAsync();
