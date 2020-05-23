using GoogleBooks.Client.Configuration.ConfigurationOptions;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Options;

namespace GoogleBooks.Client.Factories
{
    public class UrlFactory : IUrlFactory
    {
        private readonly GoogleBooksUrlOptions _options;

        public UrlFactory(IOptions<GoogleBooksUrlOptions> configuration)
        {
            _options = configuration.Value;
        }

        public string GetBookDetailsUrl(string bookId)
        {
            return $"{ _options.GetBookDetails }{ bookId }";
        }

        public string GetDefaultsBooksUrl(string keywords)
        {
            return $"{ _options.GetDefaultBooks }{ keywords }";
        }
    }
}
