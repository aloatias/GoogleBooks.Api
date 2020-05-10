using GoogleBooks.Client.Dtos;
using GoogleBooks.Client.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Services
{
    public class GoogleBooksClientService : IGoogleBooksClientService
    {
        private readonly HttpClient _httpClient;

        public GoogleBooksClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task TestClient()
        {
            var responseString = await _httpClient.GetStringAsync("volumes?q=federer");

            var books = JsonConvert.DeserializeObject<Books>(responseString);

            throw new System.NotImplementedException();
        }
    }
}
