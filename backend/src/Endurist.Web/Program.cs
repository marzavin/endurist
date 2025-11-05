using Endurist.Core.Services;
using Endurist.Hosting.Settings;
using Endurist.Web.Middlewares;
using Endurist.Web.Registrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

const string AllowedOriginsPolicy = "AllowedOrigins";

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

var authenticationConfig = new AuthenticationConfiguration();
configuration.GetSection("Authentication").Bind(authenticationConfig);
services.AddSingleton(authenticationConfig);

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = TokenProvider.GetSecurityKey(authenticationConfig.Secret),
            ValidIssuer = authenticationConfig.Issuer,
            ValidAudience = authenticationConfig.Audience,
            ClockSkew = TimeSpan.Zero
        };
    });
services.AddAuthorization();

//TODO:AMZ: Move to a separate extension file
services.AddSingleton<IEncryptionService, EncryptionService>();
services.AddSingleton<TokenProvider>();

services.AddHttpContextAccessor();
services.AddScoped<Endurist.Core.Services.ExecutionContext>();

services.AddHandlers();
services.AddWidgets();

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT"
    };

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            []
        }
    };

    options.AddSecurityRequirement(securityRequirement);
});

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
