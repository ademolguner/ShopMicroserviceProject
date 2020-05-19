using Shop.BasketService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shop.BasketService.Business.Abstract
{
    public interface IBasketServices
    {
        Task<Basket> GetAsync(int productId);

        Task<List<Basket>> GetListAsync(Expression<Func<Basket, bool>> filter);

        Task AddAsync(Basket basket);

        Task UpdateAsync(Basket basket);

        Task DeleteAsync(Basket basket);
    }
}