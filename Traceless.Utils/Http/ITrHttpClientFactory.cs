using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils.Http
{
    public interface ITrHttpClientFactory<IT, T> where T : class, IT where IT : class
    {
        IT CreateHttpClient();
    }

    public interface ITrHttpClientFactory : ITrHttpClientFactory<ITrHttpClient, TrHttpClient>
    {
    }
}