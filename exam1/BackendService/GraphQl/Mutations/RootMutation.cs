using BackendService.Abstractions;
using BackendService.Model;

namespace BackendService.GraphQl.Mutations;

public class RootMutation(IEntityService entityService):ObjectType
{
    public EntityMutation EntityMutation { get; } = new(entityService);
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field("createEntity")
            .Type<ObjectType<Entity>>()
            .Argument("name", a => a.Type<NonNullType<StringType>>())
            .Argument("description", a => a.Type<NonNullType<StringType>>())
            .Resolve(context => ValueTask.FromResult<object?>(EntityMutation.CreateEntity(context.ArgumentValue<string>("name"),
                context.ArgumentValue<string>("description"))));
        descriptor.Field("updateEntity")
            .Type<ObjectType<Entity>>()
            .Argument("id", a => a.Type<NonNullType<StringType>>())
            .Argument("name", a => a.Type<NonNullType<StringType>>())
            .Argument("description", a => a.Type<NonNullType<StringType>>())
            .Resolve(context => ValueTask.FromResult<object?>(EntityMutation.UpdateEntity(
                context.ArgumentValue<string>("id"),
                context.ArgumentValue<string>("name"),
                context.ArgumentValue<string>("description"))));
        descriptor.Field("deleteEntity")
            .Type<BooleanType>()
            .Argument("id", a => a.Type<NonNullType<StringType>>())
            .Resolve(context => ValueTask.FromResult<object?>(EntityMutation.DeleteEntity(context.ArgumentValue<string>("id"))));
    }
}