using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils.Http
{
    public class TrHttpClientFactoryBase<IT, T> : ITrHttpClientFactory<IT, T> where T : class, IT
                                                    where IT : class
    {
        private IHost _host;

        public TrHttpClientFactoryBase()
        {
            var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHttpClient();
                services.AddTransient<IT, T>();
            }).UseConsoleLifetime();
            _host = builder.Build();
        }

        public IT CreateHttpClient()
        {
            using var serviceScope = _host.Services.CreateScope();
            var service = serviceScope.ServiceProvider;
            return service.GetRequiredService<IT>();
        }
    }
}