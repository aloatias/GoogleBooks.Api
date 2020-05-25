using GoogleBooks.Client.Configuration.ConfigurationOptions;
using GoogleBooks.Client.Factories;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace GoogleBooks.Client.Integration.Tests
{
    public class TestFactory
    {
        private const string _baseUrlSectionName = "Urls:Base";
        private const string _getDefaultBooksUrl = "Urls:GetDefaultBooks";
        private const string _getBookDetailsUrl = "Urls:GetBookDetails";
        private const string _maxResultsParameter = "Urls:MaxResultsParameter";
        private const string _startIndexParameter = "Urls:StartIndexParameter";

        private IConfiguration _configuration;

        protected IGoogleBooksClientService CreateGoogleBooksClientService()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            return new GoogleBooksClientService(CreateUrlFactory(), CreateHttpClient());
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_configuration.GetSection(_baseUrlSectionName).Value.ToString())
            };

            return httpClient;
        }

        private IUrlFactory CreateUrlFactory()
        {
            var googleBooksUrls = new GoogleBooksUrlOptions
            {
                GetBookDetails = _configuration.GetSection(_getBookDetailsUrl).Value.ToString(),
                GetDefaultBooks = _configuration.GetSection(_getDefaultBooksUrl).Value.ToString(),
                MaxResultsParameter = _configuration.GetSection(_maxResultsParameter).Value.ToString(),
                StartIndexParameter = _configuration.GetSection(_startIndexParameter).Value.ToString()
            };
            IOptions<GoogleBooksUrlOptions> options = Options.Create(googleBooksUrls);

            return new UrlFactory(options);
        }
    }
}