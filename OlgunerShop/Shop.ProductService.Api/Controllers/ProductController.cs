using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Shop.ProductService.Business.Abstract;
using Shop.ProductService.Entities.Models;

namespace Shop.ProductService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var product =await _productServices.GetAsync(id);
            return Ok(product);
        }

       
        // PUT: api/Product/5
        [HttpPut]
        public async Task<IActionResult> Put(Product productItem)
        {
            var product = await _productServices.GetAsync(productItem.ProductId);
            if (product!=null)
            {
                await _productServices.UpdateAsync(productItem);
                return Ok(productItem);
            }

            return NotFound(productItem);

        }

    }
}
