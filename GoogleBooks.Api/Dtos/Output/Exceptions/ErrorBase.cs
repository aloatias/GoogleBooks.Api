using System;

namespace GoogleBooks.Api.Dtos.Output.Exceptions
{
    public abstract class ErrorBase : Exception
    {
        public override string Message { get; }

        protected ErrorBase(string message)
        {
            Message = message;
        }
    }
}
