using GoogleBooks.Client.Configuration;
using GoogleBooks.Client.Configuration.ConfigurationOptions;
using GoogleBooks.Client.Factories;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace GoogleBooks.Client.Integration.Tests
{
    public class TestFactory
    {
        protected IGoogleBooksClientService CreateGoogleBooksClientService()
        {
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //var configuration = builder.Build();

            //var serviceCollection = new ServiceCollection();
            //var serviceProvider = ServicesConfiguration.ConfigureGoogleBooksClientServices(serviceCollection, configuration);

            //return serviceProvider.GetRequiredService<IGoogleBooksClientService>();

            return new GoogleBooksClientService(CreateUrlFactory(), CreateHttpClient());
        }

        private HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }

        private IUrlFactory CreateUrlFactory()
        {   
            var urlOptions = new OptionsFactory(new GoogleBooksUrlOptions(), ).Create(IOptions<GoogleBooksUrlOptions>);

            return new UrlFactory(urlOptions.Create());
        }
    }
}