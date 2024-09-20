using GraphQL.Server.Sample.Catalog.Schema.Model;
using GraphQL.Server.Sample.Catalog.Services;

namespace GraphQL.Server.Sample.Catalog.Schema;

public class Mutation
{
    #region Item

    public static Item AddItem([FromServices] ICatalogService catalogService, ItemInput item)
        => catalogService.AddItem(item);

    public static Item? DeleteItem([FromServices] ICatalogService catalogService, string id)
        => catalogService.DeleteItem(id);

    public static int ClearItems([FromServices] ICatalogService catalogService)
        => catalogService.ClearItems();

    #endregion

    #region Manifest

    public static Manifest AddManifest([FromServices] ICatalogService catalogService, ManifestInput manifest)
        => catalogService.AddManifest(manifest);

    public static Manifest AddManifestFromAllItems([FromServices] ICatalogService catalogService, ManifestInput manifest)
        => catalogService.AddManifestFromAllItems(manifest);

    #endregion
}
