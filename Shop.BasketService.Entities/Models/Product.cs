
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Shop.Core.Entities;

namespace Shop.BasketService.Api.Entities
{
    public class Product :  IEntity
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; } 
        public decimal UnitPrice { get; set; } 
        public short UnitsInStock { get; set; }
        public string PictureUrl { get; set; }
    }
}
