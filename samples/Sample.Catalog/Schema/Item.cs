namespace GraphQL.Server.Sample.Catalog.Schema;

public class Item
{
    [Id]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedOn { get; set; }
}
