var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.rekordassistant-ai-app_ApiService>("apiservice")
    .WithEnvironment("Google__ClientId", builder.Configuration["Google:ClientId"])
    .WithEnvironment("Google__ClientSecret", builder.Configuration["Google:ClientSecret"]);

builder.AddProject<Projects.rekordassistant-ai-app_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
