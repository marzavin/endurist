var builder = DistributedApplication.CreateBuilder(args);

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin();

var mongo = builder.AddMongoDB("mongo");
var mongodb = mongo.AddDatabase("mongodb");

builder.AddProject<Projects.Endurist_Web>("webapi")
    .WithReference(rabbitmq).WaitFor(rabbitmq);

builder.AddProject<Projects.Endurist_Worker>("worker")
    .WithReference(rabbitmq).WaitFor(rabbitmq)
    .WithReference(mongodb).WaitFor(mongodb);

builder.Build().Run();
