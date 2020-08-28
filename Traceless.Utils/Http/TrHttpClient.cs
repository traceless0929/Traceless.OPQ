using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Traceless.Utils.Http
{
    public class TrHttpClient : ITrHttpClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public TrHttpClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<HttpResponseMessage> GetAsync(string url, string contentType = "application/json")
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                 new Uri(url));
            request.Headers.Clear();
            var client = _clientFactory.CreateClient();
            return client.SendAsync(request);
        }

        public Task<HttpResponseMessage> PostAsync(string url, string data, string contentType = "application/json")
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                new Uri(url));
            request.Headers.Clear();
            request.Content = new StringContent(data);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            var client = _clientFactory.CreateClient();
            return client.SendAsync(request);
        }
    }
}