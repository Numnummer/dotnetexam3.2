using BackendService.Abstractions;
using BackendService.Model;
using HotChocolate;

namespace BackendService.GraphQl.Mutations;

public class EntityMutation(IEntityService entityService)
{
    public Entity CreateEntity(
        string name,
        string description)
    {
        var entity = new Entity { Name = name, Description = description };
        return entityService.Create(entity);
    }

    public Entity? UpdateEntity(
        string id,
        string name,
        string description)
    {
        var entity = new Entity { Id = id, Name = name, Description = description };
        return entityService.Update(id, entity);
    }

    public bool DeleteEntity(string id)
    {
        return entityService.Delete(id);
    }
    
}