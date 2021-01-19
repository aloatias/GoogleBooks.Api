using System;
using System.Net;

namespace GoogleBooks.Api.Dtos
{
    public interface IActionResponse
    {
        HttpStatusCode Status { get; }
        string ErrorMessage { get; }
        Exception Exception { get; }
    }

    public interface IActionResponse<T> : IActionResponse, IContent<T> where T : class
    {
    }

    public interface IContent<T> where T : class
    {
        T Content { get; }
    }
}