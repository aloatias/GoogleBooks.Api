using GoogleBooks.Client.Configuration.ConfigurationOptions;
using GoogleBooks.Client.Factories;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace GoogleBooks.Client.Integration.Tests
{
    public class TestFactory
    {
        protected IGoogleBooksClientService CreateGoogleBooksClientService()
        {
            return new GoogleBooksClientService(CreateUrlFactory(), CreateHttpClient());
        }

        private HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }

        private IUrlFactory CreateUrlFactory()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
            
            IConfiguration configuration = builder.Build();

            var googleBooksUrls = new GoogleBooksUrlOptions
            {
                SearchDefaultBooks = configuration.GetSection("Urls:SearchDefaultBooks").Value.ToString()
            };
            IOptions<GoogleBooksUrlOptions> options = Options.Create(googleBooksUrls);

            return new UrlFactory(options);
        }
    }
}