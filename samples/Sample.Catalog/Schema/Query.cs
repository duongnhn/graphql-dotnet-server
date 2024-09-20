
using GraphQL.Server.Sample.Catalog.Services;

namespace GraphQL.Server.Sample.Catalog.Schema;

public class Query
{
    public static Item? LastItem([FromServices] ICatalogService catalogService)
        => catalogService.LastItem;

    public static IEnumerable<Item> AllItems([FromServices] ICatalogService catalogService)
        => catalogService.GetAllItems();

    public static int Count([FromServices] ICatalogService catalogService)
        => catalogService.Count;
}
