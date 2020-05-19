using MongoDB.Driver;
using NUnit.Framework;
using Shop.BasketService.Business.Concrete;
using Shop.BasketService.Entities.Models;
using Shop.Core.Amqp.Bus;
using Shop.Core.Utilities.Exceptions;
using Shop.ProductService.Business.Abstract;
using Shop.ProductService.Business.Concrete;
using Shop.ProductService.Entities.Models;
using System;
using System.Threading.Tasks;

namespace Shop.ApiProject.Test
{
    public class BasketTest
    {

        private BasketManager _basketManager;
        private readonly IMongoDatabase _database;

        [SetUp]
        public void Setup()
        {
            _basketManager = new BasketManager(_database);
        }



        [Test]
        public void Create_Basket_Item()
        {
            var basketItem = new Basket() { };
            var resultData = _basketManager.AddAsync(basketItem);
            Assert.AreEqual(resultData.Status, TaskStatus.Created);

        }

        [Test]
        public void Basket_Null_Exception()
        {
            var product = _basketManager.GetAsync(851);
            //Assert.AreEqual(product.Result.ProductName, "PC");
            Assert.Catch<ProductNotFoundException>(() => product.Result.ProductName.ToString(), "PC");
            Assert.Throws<ProductNotFoundException>(() => product.Result.ProductName.ToString(), "PC");
        }


        public void Product_Added_Control()
        {

        }
    }
}