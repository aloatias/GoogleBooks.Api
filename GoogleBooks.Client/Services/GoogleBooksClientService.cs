using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Infrastructure.Dtos;
using GoogleBooks.Infrastructure.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Services
{
    public class GoogleBooksClientService : IGoogleBooksClientService
    {
        private readonly IUrlFactory _urlFactory;
        private readonly HttpClient _httpClient;

        public GoogleBooksClientService
        (
            IUrlFactory urlFactory,
            HttpClient httpClient
        )
        {
            _urlFactory = urlFactory;
            _httpClient = httpClient;
        }

        public async Task<IActionResponse<GoogleBookDetailsFull>> GetBookDetailsAsync(string bookId)
        {
            try
            {
                _urlFactory.SetBookDetailsUrl(bookId);
                
                string response = await GetResponseStringAsync();

                return new Ok<GoogleBookDetailsFull>(DeserializeResponse<GoogleBookDetailsFull>(response));
            }
            catch (Exception ex)
            {
                return new InternalServerError<GoogleBookDetailsFull>(ex.Message, ex);
            }
        }

        public async Task<IActionResponse<GoogleBooksCatalog>> GetBooksCatalogAsync(string keywords, int pageSize, int pageNumber)
        {
            try
            {
                _urlFactory.SetBooksCatalogUrl(keywords, pageSize, pageSize * pageNumber);

                string response = await GetResponseStringAsync();

                return new Ok<GoogleBooksCatalog>(DeserializeResponse<GoogleBooksCatalog>(response));
            }
            catch (Exception ex)
            {
                return new InternalServerError<GoogleBooksCatalog>(ex.Message, ex);
            }
        }

        #region Private Methods
        private async Task<string> GetResponseStringAsync()
        {
            return await _httpClient.GetStringAsync(_urlFactory.Url);
        }

        private T DeserializeResponse<T>(string response) where T : class
        {
            return JsonConvert.DeserializeObject<T>(response);
        }
        #endregion
    }
}
