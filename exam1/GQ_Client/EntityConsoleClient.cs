using GQ_Client.Model;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace GQ_Client;

public class EntityConsoleClient
{
    private const string GraphQlUrl = "http://localhost:5056/graphql";
    private readonly GraphQLHttpClient _client = new(GraphQlUrl, new NewtonsoftJsonSerializer());

    public async Task CreateEntity()
    {
        Console.Write("Enter name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Enter description: ");
        var description = Console.ReadLine() ?? "";

        var mutation = new GraphQLRequest
        {
            Query = @"
            mutation($name: String!, $description: String!) {
                createEntity(name: $name, description: $description) {
                    id
                    name
                    description
                }
            }",
            Variables = new
            {
                name,
                description
            }
        };

        var response = await _client.SendMutationAsync<MutationResponse>(mutation);
        Console.WriteLine($"Created entity with ID: {response.Data.CreateEntity.Id}");
    } 
    public async Task ListEntities()
    {
        var query = new GraphQLRequest
        {
            Query = @"
            query {
                getEntities {
                    id
                    name
                    description
                }
            }"
        };
        var response = await _client.SendQueryAsync<GetAllResponse>(query);
        foreach (var entity in response.Data.GetEntities)
        {
            Console.WriteLine(entity);
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

        var mutation = new GraphQLRequest
        {
            Query = @"
            mutation($id: String!, $name: String!, $description: String!) {
                updateEntity(id: $id, name: $name, description: $description) {
                    id
                    name
                    description
                }
            }",
            Variables = new
            {
                id,
                name,
                description
            }
        };

        var response = await _client.SendMutationAsync<MutationResponse>(mutation);

        Console.WriteLine($"Updated entity: {response.Data.UpdateEntity}");
    }

    public async Task DeleteEntity()
    {
        Console.Write("Enter entity ID to delete: ");
        var id = Console.ReadLine() ?? "";

        var mutation = new GraphQLRequest
        {
            Query = @"
            mutation($id: String!) {
                deleteEntity(id: $id)
            }",
            Variables = new
            {
                id
            }
        };

        var response = await _client.SendMutationAsync<DeleteResponse>(mutation);
        Console.WriteLine(response.Data.DeleteEntity ? "Entity deleted" : "Entity not found");
    }
    
    // Внутренние классы для десериализации ответа
    private class GetAllResponse
    {
        public IEnumerable<Entity> GetEntities { get; set; }
    }

    private class MutationResponse
    {
        public Entity CreateEntity { get; set; }
        public Entity? UpdateEntity { get; set; }
    }

    private class DeleteResponse
    {
        public bool DeleteEntity { get; set; }
    }
}
