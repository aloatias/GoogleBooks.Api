using GoogleBooks.Api.Domain;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Infrastructure.Interfaces;
using System.Threading.Tasks;
using BooksCatalog = GoogleBooks.Api.Domain.BooksCatalog;

namespace GoogleBooks.Api.Interfaces
{
    public interface IBooksService
    {
        Task<IActionResponse<GoogleBookDetailsFull>> GetBookDetailsAsync(Book book);
        Task<IActionResponse<GoogleBooksCatalog>> GetBooksCatalogAsync(BooksCatalog catalogBooksSearch);
    }
}