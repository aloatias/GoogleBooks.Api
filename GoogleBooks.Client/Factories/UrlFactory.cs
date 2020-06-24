using GoogleBooks.Client.Configuration.ConfigurationOptions;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Options;

namespace GoogleBooks.Client.Factories
{
    public class UrlFactory : IUrlFactory
    {
        private readonly GoogleBooksUrlOptions _options;
        
        public string Url { get; private set; }

        public UrlFactory(IOptions<GoogleBooksUrlOptions> configuration)
        {
            _options = configuration.Value;
        }

        public void SetBookDetailsUrl(string bookId)
        {
            Url = $"{ _options.GetBookDetails }{ bookId }";
        }

        public void SetBooksCatalogUrl(string keywords, int pageSize, int startIndex)
        {
            Url = $"{ _options.GetBooksCatalog }{ keywords }";
            
            SetMaxResultsParameter(pageSize);
            SetStartIndexParameter(startIndex);
        }

        #region Private Methods
        private void SetMaxResultsParameter(int pageSize)
        {
            Url +=  $"{ _options.MaxResultsParameter }{ pageSize }";
        }

        private void SetStartIndexParameter(int startIndex)
        {
            Url += $"{ _options.StartIndexParameter }{ startIndex }";
        }
        #endregion
    }
}
