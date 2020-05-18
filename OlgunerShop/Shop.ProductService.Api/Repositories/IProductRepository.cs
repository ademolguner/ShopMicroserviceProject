using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.ProductService.Api.Entities;

namespace Shop.ProductService.Api.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(int productId);
        Task UpdateAsync(Product product);
    }

    
}
