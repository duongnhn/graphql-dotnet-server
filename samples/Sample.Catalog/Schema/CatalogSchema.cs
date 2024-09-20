using GraphQL.Types;

namespace GraphQL.Server.Sample.Catalog.Schema;

public class CatalogSchema : GraphQL.Types.Schema
{
    public CatalogSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = new AutoRegisteringObjectGraphType<Query>();
        Mutation = new AutoRegisteringObjectGraphType<Mutation>();
        Subscription = new AutoRegisteringObjectGraphType<Subscription>();
    }
}
