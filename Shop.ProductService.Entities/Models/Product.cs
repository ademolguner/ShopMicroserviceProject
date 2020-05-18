namespace Shop.ProductService.Entities.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
    }
}