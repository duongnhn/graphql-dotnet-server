using GraphQL.Server.Sample.Catalog.Services;

namespace GraphQL.Server.Sample.Catalog.Schema;

public class Subscription
{
    public static IObservable<Item> NewItems([FromServices] ICatalogService catalogService)
        => catalogService.SubscribeNewItem();

    public static IObservable<ItemEvent> Events([FromServices] ICatalogService catalogService)
        => catalogService.SubscribeEvents();
}
