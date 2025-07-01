using BackendService.Model;

namespace BackendService.Abstractions;

public interface IEntityService
{
    IEnumerable<Entity> GetAll();
    Entity Create(Entity entity);
    Entity? Update(string id, Entity updatedEntity);
    bool Delete(string id);
    int Count();
}