namespace PetShopClient.Models
{
    public class ProductDTO
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public Guid CategoryId { get; set; }
        public float UnitPrice { get; set; }
        public string? Image { get; set; }
        public string? Origin { get; set; }
    }
}
