using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shop.BasketService.Business.Abstract;
using Shop.BasketService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shop.BasketService.Business.Concrete
{
    public class BasketManager : IBasketServices
    {
        private readonly IMongoDatabase _database;

        public BasketManager(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<Basket> Collection
            => _database.GetCollection<Basket>("Baskets");

        public async Task<Basket> GetAsync(int id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.ProductId == id);

        public async Task<List<Basket>> GetListAsync(Expression<Func<Basket, bool>> filter)
            => await Collection
                .AsQueryable().Where(filter).ToListAsync();

        public async Task AddAsync(Basket basket)
            => await Collection.InsertOneAsync(basket);

        public Task UpdateAsync(Basket basket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Basket basket)
        {
            throw new NotImplementedException();
        }
    }
}