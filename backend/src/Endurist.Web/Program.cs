using Endurist.Core.Services;
using Endurist.Hosting.Settings;
using Endurist.Web.Middlewares;
using Endurist.Web.Registrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using SideEffect.Messaging;
using SideEffect.Messaging.Redis;
using System.Reflection;

const string AllowedOriginsPolicy = "AllowedOrigins";

Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

var builder = WebApplication.CreateBuilder(args);

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
services.AddMongoStorage(configuration);

var authConfig = services.AddConfiguration<AuthenticationConfiguration>(configuration, "Authentication");

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = TokenProvider.GetSecurityKey(authConfig.Secret),
            ValidIssuer = authConfig.Issuer,
            ValidAudience = authConfig.Audience,
            ClockSkew = TimeSpan.Zero
        };
    });
services.AddAuthorization();

//TODO:AMZ: Move to a separate extension file
services.AddSingleton<IEncryptionService, EncryptionService>();
services.AddSingleton<TokenProvider>();

services.AddConfiguration<RedisStorageConfiguration>(configuration, "RedisStorage");
services.AddSingleton<IServiceBus, RedisServiceBus>();

services.AddHttpContextAccessor();
services.AddScoped<Endurist.Core.Services.ExecutionContext>();

services.AddHandlers();
services.AddWidgets();

services.AddControllers();

services.AddEndpointsApiExplorer();

services.Configure<ForwardedHeadersOptions>(options => {
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders();

app.UseCors(AllowedOriginsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
