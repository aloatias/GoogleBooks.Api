using GoogleBooks.Client.Factories;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace GoogleBooks.Client.Configuration
{
    public static class ServicesConfiguration
    {
        public static ServiceProvider ConfigureGoogleBooksClientServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IUrlFactory, UrlFactory>();
            serviceCollection.AddHttpClient<IGoogleBooksClientService, GoogleBooksClientService>(
                client => client.BaseAddress = new Uri(configuration.GetSection("Urls:Base").Value.ToString()));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
