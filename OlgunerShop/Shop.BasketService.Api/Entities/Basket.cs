using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Core.Entities;

namespace Shop.BasketService.Api.Entities
{
    public class Basket : IEntity
    {

        public Guid _Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}
