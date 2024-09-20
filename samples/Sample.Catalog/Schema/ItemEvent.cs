namespace GraphQL.Server.Sample.Catalog.Schema;

public class ItemEvent
{
    public ItemEventType Type { get; set; }
    public Item? Item { get; set; }
}
