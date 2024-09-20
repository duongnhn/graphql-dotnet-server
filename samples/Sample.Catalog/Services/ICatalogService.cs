using GraphQL.Server.Sample.Catalog.Schema;

namespace GraphQL.Server.Sample.Catalog.Services;

public interface ICatalogService
{
    int Count { get; }
    Item? LastItem { get; }
    int ClearItems();
    Item? DeleteItem(int id);
    IEnumerable<Item> GetAllItems();
    Item PostItem(ItemInput item);
    IObservable<ItemEvent> SubscribeEvents();
    IObservable<Item> SubscribeNewItem();
}
