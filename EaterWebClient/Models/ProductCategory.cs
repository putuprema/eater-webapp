namespace EaterWebClient.Models
{
    public record SimpleProductCategory(string Id, string Name);
    public record ProductCategory(string Id, string Name, int SortIndex) : SimpleProductCategory(Id, Name);
}
