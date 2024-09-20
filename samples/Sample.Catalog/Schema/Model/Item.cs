namespace GraphQL.Server.Sample.Catalog.Schema.Model;

public class Item
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedOn { get; set; }
}
