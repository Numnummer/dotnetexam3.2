using BackendService.Abstractions;
using BackendService.Model;

namespace BackendService.GraphQl.Queries;

public class RootQuery(IEntityService entityService):ObjectType
{
    public EntityQuery EntityQuery { get; } = new EntityQuery(entityService);
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field("getEntities")
            .Type<ListType<ObjectType<Entity>>>()
            .Resolve(context =>
            {
                return ValueTask.FromResult<object?>(EntityQuery.GetEntities().ToArray());
            });
    }
}