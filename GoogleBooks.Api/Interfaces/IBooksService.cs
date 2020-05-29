using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Interfaces
{
    public interface IBooksService
    {
        Task<BookDetailsFullResult> GetBookDetailsAsync(string bookId);

        Task<BooksCatalogResult> GetBooksCatalogAsync(BooksCatalogSearch catalogBooksSearch);
    }
}