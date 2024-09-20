using GraphQL.Server.Sample.Catalog.Services;

namespace GraphQL.Server.Sample.Catalog.Schema;

public class Mutation
{
    public static Item AddItem([FromServices] ICatalogService catalogService, ItemInput Item)
        => catalogService.PostItem(Item);

    public static Item? DeleteItem([FromServices] ICatalogService catalogService, [Id] int id)
        => catalogService.DeleteItem(id);

    public static int ClearItems([FromServices] ICatalogService catalogService)
        => catalogService.ClearItems();
}
