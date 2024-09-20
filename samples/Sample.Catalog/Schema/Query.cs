
using GraphQL.Server.Sample.Catalog.Schema.Model;
using GraphQL.Server.Sample.Catalog.Services;

namespace GraphQL.Server.Sample.Catalog.Schema;

public class Query
{
    #region Item

    public static IEnumerable<Item> AllItems([FromServices] ICatalogService catalogService)
        => catalogService.GetAllItems();

    public static int Count([FromServices] ICatalogService catalogService)
        => catalogService.Count;

    public static Item? GetItem([FromServices] ICatalogService catalogService, string id)
        => catalogService.GetItemById(id);

    #endregion

    #region Manifest
    public static IEnumerable<Manifest> AllManifests([FromServices] ICatalogService catalogService)
        => catalogService.GetAllManifests();

    public static Manifest? GetManifest([FromServices] ICatalogService catalogService, string id)
        => catalogService.GetManifestById(id);

    #endregion
}
