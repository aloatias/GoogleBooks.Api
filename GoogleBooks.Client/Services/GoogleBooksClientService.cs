using GoogleBooks.Client.Dtos;
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

        public async Task<BookDetailsFull> GetBookDetailsByIdAsync(string bookId)
        {
            var url = _urlFactory.GetBookDetailsUrl(bookId);
            var responseString = await _httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<BookDetailsFull>(responseString);
        }

        public async Task<Books> GetBooksByKeywordAsync(string keywords)
        {
            var url = _urlFactory.GetDefaultsBooksUrl(keywords);
            var responseString = await _httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<Books>(responseString);
        }
    }
}
