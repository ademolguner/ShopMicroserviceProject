using Shop.Core.Amqp.Bus;
using Shop.Core.Utilities.Exceptions;
using Shop.Domain.Commands;
using Shop.ProductService.Business.Abstract;
using Shop.ProductService.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ProductService.Business.Concrete
{
    public class ProductManager : IProductServices
    {
        private IEventBus eventBus;

        public ProductManager(IEventBus eventBus)
        {
            this.eventBus = eventBus;
            LoadProductList();
        }

        public async Task<Product> GetAsync(int productId)
        {
            var product = LoadProductList().FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
                return await Task.FromResult<Product>(LoadProductList().FirstOrDefault(p => p.ProductId == productId));
            return await Task.FromException<Product>(new ProductNotFoundException());
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
            List<Product> ProductList = new List<Product>
            {
                new Product()
                {
                    ProductId = 1,
                    ProductName = "PC",
                    CategoryId = 1,
                    PictureUrl = "1.jpg",
                    UnitsInStock = 3,
                    UnitPrice = 1000
                },
                new Product()
                {
                    ProductId = 2,
                    ProductName = "Printer",
                    CategoryId = 1,
                    PictureUrl = "2.jpg",
                    UnitsInStock = 13,
                    UnitPrice = 2000
                },
                new Product()
                {
                    ProductId = 3,
                    ProductName = "Tablet",
                    CategoryId = 1,
                    PictureUrl = "3.jpg",
                    UnitsInStock = 2,
                    UnitPrice = 3000
                },
                new Product()
                {
                    ProductId = 4,
                    ProductName = "Phone",
                    CategoryId = 1,
                    PictureUrl = "4.jpg",
                    UnitsInStock = 4,
                    UnitPrice = 4000
                }
            };

            return ProductList;
        }
    }
}