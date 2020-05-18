﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.ProductService.Api.Entities;
using Shop.ProductService.Api.Repositories;

namespace Shop.ProductService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var product =await _productRepository.GetAsync(id);
            return Ok(product);
        }

       
        // PUT: api/Product/5
        [HttpPut]
        public async Task<IActionResult> Put(Product productItem)
        {
            var product = await _productRepository.GetAsync(productItem.ProductId);
            if (product!=null)
            {
                await _productRepository.UpdateAsync(productItem);
                return Ok(productItem);
            }

            return NotFound(productItem);

        }

    }
}