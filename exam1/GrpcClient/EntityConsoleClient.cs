using BackendService;
using Grpc.Net.Client;

namespace GrpcClient;

public class EntityConsoleClient
{
    private readonly EntityGrpc.EntityGrpcClient _client;

    public EntityConsoleClient()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5057");
        _client = new EntityGrpc.EntityGrpcClient(channel);
    }
    public async Task CreateEntity()
    {
        Console.Write("Enter name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Enter description: ");
        var description = Console.ReadLine() ?? "";

        var response = await _client.CreateEntityAsync(new CreateEntityRequest
        {
            Name = name,
            Description = description
        });

        Console.WriteLine($"Created entity with ID: {response.Id}");
    } 
    public async Task ListEntities()
    {
        var response = await _client.GetAllEntitiesAsync(new GetAllEntitiesRequest());

        Console.WriteLine($"Total entities: {response.TotalCount}");
        foreach (var entity in response.Entities)
        {
            Console.WriteLine($"{entity.Id}: {entity.Name} - {entity.Description}");
        }
    }
    public async Task UpdateEntity()
    {
        Console.Write("Enter entity ID to update: ");
        var id = Console.ReadLine() ?? "";
        Console.Write("Enter new name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Enter new description: ");
        var description = Console.ReadLine() ?? "";

        var response = await _client.UpdateEntityAsync(new UpdateEntityRequest
        {
            Id = id,
            Name = name,
            Description = description
        });

        Console.WriteLine($"Updated entity: {response.Id}, {response.Name}, {response.Description}");
    }

    public async Task DeleteEntity()
    {
        Console.Write("Enter entity ID to delete: ");
        var id = Console.ReadLine() ?? "";

        var response = await _client.DeleteEntityAsync(new DeleteEntityRequest { Id = id });
        Console.WriteLine(response.Success ? "Entity deleted" : "Entity not found");
    }
}