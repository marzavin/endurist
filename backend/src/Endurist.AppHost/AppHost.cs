var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("keycloak", 9191)
    .WithDataVolume();

builder.AddProject<Projects.Endurist_Web>("webapi")
    .WithReference(keycloak)
    .WaitFor(keycloak);

builder.AddProject<Projects.Endurist_Worker>("worker");

builder.Build().Run();
