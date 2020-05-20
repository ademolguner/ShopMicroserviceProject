using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using Shop.ProductService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Shop.ApiGateway.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiGatewayController : ControllerBase
    {

        private readonly ILogger<ApiGatewayController> _logger;
        public ApiGatewayController(ILogger<ApiGatewayController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string[] GetAsync()
        {

            var client = new RestClient("https://localhost:5000/api/product");
            var request = new RestRequest("/1", Method.GET);

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var returnData = JsonConvert.DeserializeObject<Product>(response.Content);
            }
            else
            { 

            }
             return new[] { "Adem", "Olguner", "Cicek Sepeti", "Proje" };
        }

        [HttpPost("AddProduct")]
        public void AddProduct()
        {

        }


    }
}