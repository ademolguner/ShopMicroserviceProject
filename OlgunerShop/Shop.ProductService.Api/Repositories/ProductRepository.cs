using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.Core.Amqp.Bus;
using Shop.Domain.Commands;
using Shop.ProductService.Api.Entities;

namespace Shop.ProductService.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private IEventBus eventBus;
        public ProductRepository(IEventBus eventBus)
        {
            this.eventBus = eventBus;
            LoadProductList();
        }
        public async Task<Product> GetAsync(int productId)
        {
            return LoadProductList().FirstOrDefault(p => p.ProductId == productId);
        }

        public async Task UpdateAsync(Product product)
        {
            var command = new ProductChangeCommand(product.ProductId,
                product.ProductName,
                product.CategoryId,
                product.UnitPrice,
                product.UnitsInStock,
                product.PictureUrl
            );
            await eventBus.SendCommand(command);
        }


        private List<Product> LoadProductList()
        {

            List<Product> ProductList = new List<Product>();
            ProductList.Add(new Product()
            {
                ProductId = 1,
                ProductName = "PC",
                CategoryId = 1,
                PictureUrl = "1.jpg",
                UnitsInStock = 3,
                UnitPrice = 1000
            });
            ProductList.Add(new Product()
            {
                ProductId = 2,
                ProductName = "Printer",
                CategoryId = 1,
                PictureUrl = "2.jpg",
                UnitsInStock = 13,
                UnitPrice = 2000
            });
            ProductList.Add(new Product()
            {
                ProductId = 3,
                ProductName = "Tablet",
                CategoryId = 1,
                PictureUrl = "3.jpg",
                UnitsInStock = 2,
                UnitPrice = 3000
            });
            ProductList.Add(new Product()
            {
                ProductId = 4,
                ProductName = "Phone",
                CategoryId = 1,
                PictureUrl = "4.jpg",
                UnitsInStock = 4,
                UnitPrice = 4000
            });

            return ProductList;
        }
    }
}
