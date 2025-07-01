using BackendService.Abstractions;
using BackendService.Model;
using BackendService.Services;
using HotChocolate;

namespace BackendService.GraphQl.Queries;

public class EntityQuery(IEntityService entityService)
{
    public IEnumerable<Entity> GetEntities()
    {
        return entityService.GetAll();
    }
}