using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Services
{
    public class BooksService : IBooksService
    {
        private readonly IGoogleBooksClientService _googleBooksClientService;

        public BooksService(IGoogleBooksClientService googleBooksClientService)
        {
            _googleBooksClientService = googleBooksClientService;
        }

        public async Task<Books> GetBooksByKeywordAsync(string keywords, int maxResults)
        {
            // TODO: Validate keywords before calling google client

            return await _googleBooksClientService.GetBooksByKeywordAsync(keywords, maxResults);
        }
    }
}
