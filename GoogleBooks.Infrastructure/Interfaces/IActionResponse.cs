using System.Net;

namespace GoogleBooks.Infrastructure.Interfaces
{
    public interface IActionResponse : IFailure
    {
        HttpStatusCode Status { get; }
    }

    public interface IActionResponse<T> : IActionResponse, IContent<T> where T : class
    {
    }
}