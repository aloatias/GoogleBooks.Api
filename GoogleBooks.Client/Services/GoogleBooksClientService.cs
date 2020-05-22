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

        public GoogleBooksClientService(IUrlFactory urlFactory,HttpClient httpClient)
        {
            _urlFactory = urlFactory;
            _httpClient = httpClient;
        }

        public async Task<Books> GetDefaultBooks(string keywords)
        {
            var url = _urlFactory.GetSearchDefaultsBooksUrl(keywords);
            var responseString = await _httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<Books>(responseString);
        }
    }
}
