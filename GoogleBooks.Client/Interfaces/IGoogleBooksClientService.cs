using GoogleBooks.Client.Dtos.Output;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Interfaces
{
    public interface IGoogleBooksClientService
    {
        Task<GoogleBookDetailsFull> GetBookDetailsByIdAsync(string bookId);

        Task<Dtos.Output.GoogleBooksCatalog> GetBooksByKeywordAsync(string keywords, int maxResults);
    }
}
