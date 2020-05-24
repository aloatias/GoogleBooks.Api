using GoogleBooks.Client.Dtos.Output;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Interfaces
{
    public interface IBooksService
    {
        Task<Books> GetBooksByKeywordAsync(string keywords, int maxResults);
    }
}