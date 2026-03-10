var builder = DistributedApplication.CreateBuilder(args);

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin();

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume();

var mongodb = mongo.AddDatabase("mongodb");

builder.AddProject<Projects.Endurist_Web>("endurist-api")
    .WithUrl("/swagger")
    .WithReference(rabbitmq).WaitFor(rabbitmq);

builder.AddProject<Projects.Endurist_Writer>("endurist-writer")
    .WithReference(rabbitmq).WaitFor(rabbitmq)
    .WithReference(mongodb).WaitFor(mongodb);

builder.AddProject<Projects.Endurist_Reader>("endurist-reader")
    .WithReference(rabbitmq).WaitFor(rabbitmq)
    .WithReference(mongodb).WaitFor(mongodb);

await builder.Build().RunAsync();
