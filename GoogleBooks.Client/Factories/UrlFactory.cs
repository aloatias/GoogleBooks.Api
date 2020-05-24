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

        public string Url { get; private set; }

        public void SetBookDetailsUrl(string bookId)
        {
            Url = $"{ _options.GetBookDetails }{ bookId }";
        }

        public void SetBookCatalogSearchUrl(string keywords)
        {
            Url = $"{ _options.GetDefaultBooks }{ keywords }";
        }

        public void SetMaxResultsParameter(int maxResults)
        {
            Url +=  $"{ _options.MaxResultsParameter }{ maxResults }";
        }
    }
}
