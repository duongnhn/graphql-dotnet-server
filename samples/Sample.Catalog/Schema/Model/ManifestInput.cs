namespace GraphQL.Server.Sample.Catalog.Schema.Model;

public class ManifestInput
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<string> ItemIds { get; set; } = new();
}
