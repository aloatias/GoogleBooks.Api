using GoogleBooks.Client.Configuration;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace GoogleBooks.Client.Integration.Tests
{
    public class TestFactory
    {
        protected IGoogleBooksClientService CreateGoogleBooksClientService()
        {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = ServicesConfiguration.ConfigurateGoogleBooksClientServices(serviceCollection);
            
            return serviceProvider.GetRequiredService<IGoogleBooksClientService>();
        }

        private HttpClient CreateHttpClient()
        {
            throw new NotImplementedException();
        }
    }
}