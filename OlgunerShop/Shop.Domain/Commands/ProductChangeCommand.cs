using Shop.Core.Amqp.Commands;
using System;

namespace Shop.Domain.Commands
{
    public class ProductChangeCommand : Command
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string PictureUrl { get; set; }

        public ProductChangeCommand(int productId, string productName, int categoryId, decimal unitPrice, short unitsInStock, string pictureUrl)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            CategoryId = categoryId;
            UnitsInStock = unitsInStock;
            PictureUrl = pictureUrl;
        }
    }
}