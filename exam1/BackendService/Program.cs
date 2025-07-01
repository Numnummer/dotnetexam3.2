using BackendService.Abstractions;
using BackendService.GraphQl.Mutations;
using BackendService.GraphQl.Queries;
using BackendService.Model.GraphQl;
using BackendService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<IEntityService, EntityService>();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<RootQuery>()
    .AddMutationType<RootMutation>()
    .AddType<EntityType>();
builder.WebHost.ConfigureKestrel(options => {
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

var app = builder.Build();

app.MapGrpcService<EntityGrpcService>();
app.MapGraphQL();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();