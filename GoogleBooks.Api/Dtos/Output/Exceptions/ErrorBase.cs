using System;

namespace GoogleBooks.Api.Dtos.Output.Exceptions
{
    public abstract class ErrorBase : Exception
    {
        public string ErrorMessage { get; private set; }

        protected ErrorBase(string message)
        {
            ErrorMessage = message;
        }
    }
}
