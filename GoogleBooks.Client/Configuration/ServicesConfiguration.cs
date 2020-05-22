using GoogleBooks.Client.Interfaces;
using GoogleBooks.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace GoogleBooks.Client.Configuration
{
    public static class ServicesConfiguration
    {
        public static ServiceProvider ConfigureGoogleBooksClientServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IGoogleBooksClientService, GoogleBooksClientService>(
                client => client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/"));

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
