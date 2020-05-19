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



        [TestCase(1, "PC")]
        [TestCase(2, "PC")]
        public void Product_Find(int productId, string expectedProductName)
        {
            var product = _productManager.GetAsync(productId);
            Assert.AreEqual(product.Result.ProductName, expectedProductName);
        }
         

        [TestCase(1)]
        [TestCase(11)]
        public void Product_FindBy_Is_Not_Null(int productId)
        {
            var product = _productManager.GetAsync(productId);
            Assert.IsNotNull(product.Result);

        }


        //[TestCase(1, typeof(ArgumentNullException))]
        //[TestCase(2, typeof(ArgumentException))]
        //[TestCase(13, typeof(ProductNotFoundException))]
        //public  void Product_Find_Exception(int id, Type expectedException)
        //{
        //    var data = _productManager.GetAsync(id);
        //    Assert.Throws(expectedException, () => _productManager.GetAsync(id));
        //}





        [TestCase(2)]
        public void Product_Update_Is_EventHandler(int productId)
        {
            var product = _productManager.GetAsync(productId);
            product.Result.ProductName = "Urun Adi Degsti";
            // _productManager.UpdateAsync(product.Result);
        }

        // [Test]
        //public void Product_Added_Control()
        //{
        //    // yeni product ekleme buraya mı yoksa basket a mı yazılacak bakacaz
        //}
    }
}