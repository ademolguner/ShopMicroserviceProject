using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.ApiGateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdemOlgunerController : ControllerBase
    {

        private readonly ILogger<AdemOlgunerController> _logger;
        public AdemOlgunerController(ILogger<AdemOlgunerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string[] ShoppingOperationKeywords()
        {
            return new[] { "Adem", "Olguner", "Cicek Sepeti", "Proje" };
        }
    }
}