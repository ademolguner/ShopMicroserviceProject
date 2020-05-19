using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.ApiGateway.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdemOlgunerController : ControllerBase
    {

        private readonly ILogger<AdemOlgunerController> _logger;
        public AdemOlgunerController(ILogger<AdemOlgunerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string[] Get()
        {
            return new[] { "Adem", "Olguner", "Cicek Sepeti", "Proje" };
        }

        [HttpPost("AddProduct")]
        public void AddProduct()
        {

        }
        [HttpPut("UpdateProduct")]
        public void UpdateProduct()
        {

        }

        [HttpGet("GetProduct")]
        public void GetProduct()
        {

        }


        [HttpGet("GetBasket")]
        public void GetBasket()
        {

        }

        [HttpGet("GetBasketProducts")]
        public void GetBasketProducts()
        {

        }

    }
}