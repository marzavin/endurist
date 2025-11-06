var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Endurist_Web>("webapi");
builder.AddProject<Projects.Endurist_Worker>("worker");

builder.Build().Run();
