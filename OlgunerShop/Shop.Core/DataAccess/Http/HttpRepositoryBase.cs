
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shop.Core.DataAccess.Http
{
    public class HttpRepositoryBase<TEntity, TContext> : IHttpRepository<TEntity>
        where TEntity : class, new()
        where TContext : HttpClient, new()
    {


        public async Task<TEntity> GetJsonAsync(string uri)
        {
            using (var client = new TContext())
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                var response = await client.SendAsync(requestMessage);
                var message = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TEntity>(message);
                return result;
            }
        }

        public async Task<HttpResponseMessage> PutJsonAsync<T>(string uri, T data)
        {
            using (var client = new TContext())
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Post,
                };
                var response = await client.SendAsync(requestMessage);
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }
                return response;
            }
        }
    }
}
