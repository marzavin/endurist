var builder = DistributedApplication.CreateBuilder(args);

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin();

var mongo = builder.AddMongoDB("mongo");
var mongodb = mongo.AddDatabase("mongodb");

builder.AddProject<Projects.Endurist_Web>("endurist-api")
    .WithReference(rabbitmq).WaitFor(rabbitmq);

builder.AddProject<Projects.Endurist_Writer>("endurist-writer")
    .WithReference(rabbitmq).WaitFor(rabbitmq)
    .WithReference(mongodb).WaitFor(mongodb);

builder.AddProject<Projects.Endurist_Reader>("endurist-reader")
    .WithReference(rabbitmq).WaitFor(rabbitmq)
    .WithReference(mongodb).WaitFor(mongodb);

builder.Build().Run();
