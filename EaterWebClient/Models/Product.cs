namespace EaterWebClient.Models
{
    public record Product(
        string Id,
        string Name,
        string Description,
        long Price,
        string ImageUrl,
        SimpleProductCategory Category,
        bool Enabled
    );
}
