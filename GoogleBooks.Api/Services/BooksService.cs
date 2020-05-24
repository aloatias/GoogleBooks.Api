using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Services
{
    public class BooksService : IBooksService
    {
        private readonly IGoogleBooksClientService _googleBooksClient;

        public BooksService(IGoogleBooksClientService googleBooksClient)
        {
            _googleBooksClient = googleBooksClient;
        }

        public async Task<Books> GetBooksByKeywordAsync(string keywords, int maxResults)
        {
            return await _googleBooksClient.GetBooksByKeywordAsync(keywords, maxResults);
        }
    }
}
