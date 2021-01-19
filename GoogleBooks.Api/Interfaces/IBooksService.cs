using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
using System.Threading.Tasks;
using BooksCatalog = GoogleBooks.Api.Domain.BooksCatalog;

namespace GoogleBooks.Api.Interfaces
{
    public interface IBooksService
    {
        Task<IActionResponse<IndividualBookDetails>> GetBookDetailsAsync(Book book);

        Task<IActionResponse<BooksCatalogResult>> GetBooksCatalogAsync(BooksCatalog catalogBooksSearch);
    }
}