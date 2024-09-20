using GraphQL.Server.Sample.Catalog.Schema.Model;

namespace GraphQL.Server.Sample.Catalog.Services;

public interface ICatalogService
{
    #region Item

    int Count { get; }
    int ClearItems();
    Item? DeleteItem(string id);
    IEnumerable<Item> GetAllItems();
    Item AddItem(ItemInput item);
    Item? GetItemById(string id);

    IObservable<ItemEvent> SubscribeEvents();
    IObservable<Item> SubscribeNewItem();

    #endregion

    #region Manifest

    IEnumerable<Manifest> GetAllManifests();
    Manifest AddManifest(ManifestInput manifest);
    Manifest? GetManifestById(string id);
    Manifest AddManifestFromAllItems(ManifestInput manifest);

    #endregion
}
