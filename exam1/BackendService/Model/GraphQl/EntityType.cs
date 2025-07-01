using HotChocolate.Types;

namespace BackendService.Model.GraphQl;

public class EntityType : ObjectType<Entity>
{
    protected override void Configure(IObjectTypeDescriptor<Entity> descriptor)
    {
        descriptor.Field(e => e.Id).Type<NonNullType<IdType>>();
        descriptor.Field(e => e.Name).Type<NonNullType<StringType>>();
        descriptor.Field(e => e.Description).Type<StringType>();
    }
}