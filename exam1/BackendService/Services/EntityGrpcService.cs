using BackendService.Abstractions;
using BackendService.Model;

namespace BackendService.Services;
using Grpc.Core;

public class EntityGrpcService(IEntityService entityService): EntityGrpc.EntityGrpcBase
{
    public override Task<EntityListResponse> GetAllEntities(GetAllEntitiesRequest request, ServerCallContext context)
    {
        var entities = entityService.GetAll();
        var response = new EntityListResponse
        {
            TotalCount = entityService.Count()
        };

        response.Entities.AddRange(entities.Select(MapToResponse));
        return Task.FromResult(response);
    }

    public override Task<EntityResponse> UpdateEntity(UpdateEntityRequest request, ServerCallContext context)
    {
        var entity = new Entity
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description
        };

        var updatedEntity = entityService.Update(request.Id, entity);
        if (updatedEntity == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Entity not found"));

        return Task.FromResult(MapToResponse(updatedEntity));
    }

    public override Task<DeleteEntityResponse> DeleteEntity(DeleteEntityRequest request, ServerCallContext context)
    {
        var result = entityService.Delete(request.Id);
        return Task.FromResult(new DeleteEntityResponse { Success = result });
    }

    public override Task<EntityResponse> CreateEntity(CreateEntityRequest request, ServerCallContext context)
    {
        var entity = new Entity
        {
            Name = request.Name,
            Description = request.Description
        };

        var createdEntity = entityService.Create(entity);
        return Task.FromResult(MapToResponse(createdEntity));
    }

    private static EntityResponse MapToResponse(Entity entity)
    {
        return new EntityResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        };
    }
}