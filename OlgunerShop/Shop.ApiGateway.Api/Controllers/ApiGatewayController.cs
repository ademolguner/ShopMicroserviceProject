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
        public async System.Threading.Tasks.Task<string[]> GetAsync()
        {

            var client = new RestClient("https://localhost:5000/api/product");
            var request = new RestRequest("/1", Method.GET); 
            try
            {
            
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var returnData = JsonConvert.DeserializeObject<Product>(response.Content); 
                }
                else
                {
                    
                        try
                        {using (var client2 = new HttpClient())
                    {
                        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5000/api/product/1");
                        var response2 =  await client2.SendAsync(requestMessage);
                        var message2 = await response2.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Product>(message2.ToString());
                        // return result2;

 }
                        }
                        catch (Exception error)
                        {
                        }
                   
                }
            }
            catch (Exception error)
            {

            }



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