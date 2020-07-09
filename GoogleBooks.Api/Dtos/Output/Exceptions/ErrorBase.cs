using System;

namespace GoogleBooks.Api.Dtos.Output.Exceptions
{
    public abstract class ErrorBase : Exception
    {
        public string Message { get; private set; }

        public ErrorBase(string message)
        {
            Message = message;
        }
    }
}
