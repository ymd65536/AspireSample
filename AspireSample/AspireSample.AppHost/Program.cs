var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var apiService = builder.AddProject<Projects.AspireSample_ApiService>("apiservice");

builder.AddProject<Projects.AspireSample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.AddContainer("prometheus", "prom/prometheus")
    .WithBindMount("../prometheus", "/etc/prometheus")
    .WithHttpEndpoint(9090, targetPort: 9090);

builder.AddContainer("grafana", "grafana/grafana")
    .WithHttpEndpoint(3000, targetPort: 3000, name: "grafanaPortal");

builder.Build().Run();
