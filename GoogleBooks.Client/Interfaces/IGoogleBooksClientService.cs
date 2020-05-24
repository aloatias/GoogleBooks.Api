using GoogleBooks.Client.Dtos.Output;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Interfaces
{
    public interface IGoogleBooksClientService
    {
        Task<BookDetailsFull> GetBookDetailsByIdAsync(string bookId);

        Task<Books> GetBooksByKeywordAsync(string keywords, int maxResults);
    }
}
