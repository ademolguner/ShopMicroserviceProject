using NUnit.Framework;
using Shop.Core.Amqp.Bus;
using Shop.Core.Utilities.Exceptions;
using Shop.ProductService.Business.Abstract;
using Shop.ProductService.Business.Concrete;
using Shop.ProductService.Entities.Models;
using System;

namespace Shop.ApiProject.Test
{
    public class ProductTest
    {

        private ProductManager _productManager;
        private readonly IEventBus eventBus;

        [SetUp]
        public void Setup()
        {
            _productManager = new ProductManager(eventBus);
        }

        

        [Test]
        public void Product_FindBy_Null_Or_Default()
        {
            var product= _productManager.GetAsync(1);
            Assert.AreEqual(product.Result.ProductName, "PC");
           
        }

        [Test]
        public void Product_FindBy_Null_Or_Default_Exception()
        {
            var product = _productManager.GetAsync(851);
            //Assert.AreEqual(product.Result.ProductName, "PC");
            Assert.Catch<ProductNotFoundException>(() => product.Result.ProductName.ToString(), "PC");
            Assert.Throws<ProductNotFoundException>(() => product.Result.ProductName.ToString(), "PC");
        }


        public void Product_Added_Control()
        {

        }
    }
}