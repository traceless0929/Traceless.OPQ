using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Traceless.Utils.Http
{
    public interface ITrHttpClient
    {
        Task<HttpResponseMessage> PostAsync(string url, string data, string contentType = "application/json");

        Task<HttpResponseMessage> GetAsync(string url);
    }
}