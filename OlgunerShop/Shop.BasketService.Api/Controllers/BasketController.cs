using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using InfoQ.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shop.BasketService.Api.Configurations.Infrastructure;
using Shop.BasketService.Api.Entities;
using Shop.BasketService.Api.Repositories;
using Shop.Core.DataAccess.Http;

namespace Shop.BasketService.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IHttpClient _httpClient;
        private readonly BaseOptions _options;
        private readonly IBasketRepository _basketRepository;
        public BasketController(
            IHttpClient httpClient, 
            IOptions<BaseOptions> options, IBasketRepository basketRepository)
        {
            _httpClient = httpClient;
            _basketRepository = basketRepository;
            _options = options.Value;
        }
      

        // PUT: api/Basket/5
        [HttpPost]
        public async Task<IActionResult> Post(Basket basketItem)
        {
            var productServiceEndpoint = _options.Services.FirstOrDefault(p => p.ServiceName == "Product");

            var result = await _httpClient.GetStringAsync($"{productServiceEndpoint?.ServicePath}/api/product/{basketItem.ProductId}");
            var product = JsonConvert.DeserializeObject<Product>(result);
            if (product.UnitsInStock>0)
            {
                var basket=new Basket()
                {
                    _Id = Guid.NewGuid(),
                    ProductId = product.ProductId,
                    UnitPrice = product.UnitPrice,
                    ProductName = product.ProductName,
                    PictureUrl = product.PictureUrl,
                    UnitsInStock = product.UnitsInStock,
                    //Adem sen doldur
                };
                await _basketRepository.AddAsync(basket);
                return Ok(basket);
            }

            return BadRequest(Task.FromException(new Exception("Stokta ürün yok")));
        }

      
    }
}
