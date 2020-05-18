using Shop.ProductService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ProductService.Business.Abstract
{
   public interface IProductServices
    {
        Task<Product> GetAsync(int productId);
        Task UpdateAsync(Product product);
    }
}
