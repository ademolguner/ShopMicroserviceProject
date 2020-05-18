using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shop.BasketService.Api.Entities;

namespace Shop.BasketService.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IMongoDatabase _database;
        public BasketRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<Basket> Collection
        {
            get
            {
                return _database.GetCollection<Basket>("Baskets");
            }
        }

        public async Task<Basket> GetAsync(int id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.ProductId == id);

        public async Task<List<Basket>> GetListAsync(Expression<Func<Basket, bool>> filter)
            => await Collection
                .AsQueryable().Where(filter).ToListAsync();

        public async Task AddAsync(Basket basket)
        {
            var productInBasket = await GetAsync(basket.ProductId);
            if (productInBasket == null)
            {
                await Collection.InsertOneAsync(basket);
            }
            else
            {
                productInBasket.Quantity++;
                await UpdateAsync(productInBasket);
            }
        }


        public async Task UpdateAsync(Basket basket)
        {
            var filter = Builders<Basket>.Filter.Eq("productId", basket.ProductId);
            var update = Builders<Basket>.Update
                .Set("pictureUrl", basket.PictureUrl)
                .Set("productName", basket.ProductName);
            /////Adem devam et bir zahmet ;
            await Collection.UpdateOneAsync(filter, update);
        }

        public Task DeleteAsync(Basket basket)
        {
            throw new NotImplementedException();
        }
    }
}
