using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.BasketService.Api.Entities;
namespace Shop.BasketService.Api.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> GetAsync(int productId);
        Task<List<Basket>> GetListAsync(Expression<Func<Basket, bool>> filter);
        Task AddAsync(Basket basket);
        Task UpdateAsync(Basket basket);
        Task DeleteAsync(Basket basket);
    }
}
