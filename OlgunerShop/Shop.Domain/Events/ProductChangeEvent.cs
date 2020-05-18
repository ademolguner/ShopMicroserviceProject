using Shop.Core.Amqp.Events;
using System;

namespace Shop.Domain.Amqp.Events
{
    public class ProductChangeEvent : Event
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string PictureUrl { get; set; }

        public ProductChangeEvent(Guid id, int productId, string productName, int categoryId, decimal unitPrice, short unitsInStock, string pictureUrl)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            CategoryId = categoryId;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
            PictureUrl = pictureUrl;
        }
    }
}