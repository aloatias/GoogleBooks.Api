using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Dtos.Output;
using System.Threading.Tasks;
using BooksCatalog = GoogleBooks.Api.Domain.BooksCatalog;

namespace GoogleBooks.Api.Interfaces
{
    public interface IBooksService
    {
        Task<IndividualBookDetailsResult> GetBookDetailsAsync(Book book);

        Task<BooksCatalogResult> GetBooksCatalogAsync(BooksCatalog catalogBooksSearch);
    }
}