using BackendService.Abstractions;
using BackendService.Model;

namespace BackendService.Services;

public class EntityService : IEntityService
{
    /// <summary>
    /// In-memory хранилище
    /// </summary>
    private readonly List<Entity> _entities = [];
    
    public IEnumerable<Entity> GetAll()
        => _entities;

    public Entity Create(Entity entity)
    {
        _entities.Add(entity);
        return entity;
    }

    public Entity? Update(string id, Entity updatedEntity)
    {
        var existingEntity = GetById(id);
        if (existingEntity == null) return null;

        existingEntity.Name = updatedEntity.Name;
        existingEntity.Description = updatedEntity.Description;
        return existingEntity;
    }

    public bool Delete(string id)
    {
        var entity = GetById(id);
        return entity != null && _entities.Remove(entity);
    }
    
    private Entity? GetById(string id)
    {
        return _entities.FirstOrDefault(e => e.Id == id);
    }
    
    public int Count() => _entities.Count;
}