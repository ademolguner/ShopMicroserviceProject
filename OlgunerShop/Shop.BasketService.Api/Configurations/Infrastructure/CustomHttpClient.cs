using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Shop.BasketService.Api.Configurations.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;
        private ILogger<CustomHttpClient> _logger;
        public CustomHttpClient(ILogger<CustomHttpClient> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }
        public async Task<string> GetStringAsync(string uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, item);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await _httpClient.SendAsync(requestMessage);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item)
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, item);
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either POST or PUT" + nameof(method));
            }

            var requestMessage = new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
            };
            var responseMessage = await _httpClient.SendAsync(requestMessage);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return responseMessage;
        }
    }
}
