using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Interfaces
{
    public interface IGoogleBooksClientService
    {
        Task<IActionResponse<GoogleBookDetailsFull>> GetBookDetailsAsync(string bookId);
        Task<IActionResponse<GoogleBooksCatalog>> GetBooksCatalogAsync(string keywords, int pageSize, int pageNumber);
    }
}
