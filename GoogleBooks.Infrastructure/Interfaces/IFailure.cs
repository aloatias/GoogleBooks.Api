using System;

namespace GoogleBooks.Infrastructure.Interfaces
{
    public interface IFailure
    {
        string ErrorMessage { get; }
        Exception Exception { get; }
    }
}