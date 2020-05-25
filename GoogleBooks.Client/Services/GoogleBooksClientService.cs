using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Services
{
    public class GoogleBooksClientService : IGoogleBooksClientService
    {
        private readonly IUrlFactory _urlFactory;
        private readonly HttpClient _httpClient;

        public GoogleBooksClientService(IUrlFactory urlFactory, HttpClient httpClient)
        {
            _urlFactory = urlFactory;
            _httpClient = httpClient;
        }

        public async Task<GoogleBookDetailsFull> GetBookDetailsAsync(string bookId)
        {
            _urlFactory.SetBookDetailsUrl(bookId);
            var responseString = await _httpClient.GetStringAsync(_urlFactory.Url);

            return JsonConvert.DeserializeObject<GoogleBookDetailsFull>(responseString);
        }

        public async Task<GoogleBooksCatalog> GetBooksCatalogAsync(string keywords, int pageSize, int pageNumber)
        {
            _urlFactory.SetBooksCatalogUrl(keywords);
            _urlFactory.SetMaxResultsParameter(pageSize);
            _urlFactory.SetStartIndexParameter(pageSize * pageNumber);

            var responseString = await _httpClient.GetStringAsync(_urlFactory.Url);

            return JsonConvert.DeserializeObject<GoogleBooksCatalog>(responseString);
        }
    }
}
