using GoogleBooks.Client.Dtos;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Interfaces
{
    public interface IGoogleBooksClientService
    {
        Task<Books> TestClient();
    }
}
