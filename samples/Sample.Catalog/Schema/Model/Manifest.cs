namespace GraphQL.Server.Sample.Catalog.Schema.Model;

public class Manifest
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<string> ItemIds { get; set; } = new();

    public List<Item> Items { get; set; } = new();

    public DateTime CreatedOn { get; set; }

}
