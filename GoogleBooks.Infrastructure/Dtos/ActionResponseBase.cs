using GoogleBooks.Infrastructure.Interfaces;
using System;
using System.Net;

namespace GoogleBooks.Infrastructure.Dtos
{
    public abstract class ActionResponseBase : IActionResponse
    {
        public HttpStatusCode Status { get; private set; }
        public string ErrorMessage { get; private set; }
        public Exception Exception { get; private set; }

        protected ActionResponseBase()
        {
        }

        protected ActionResponseBase(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        protected ActionResponseBase(string errorMessage, Exception exception)
        {
            ErrorMessage = errorMessage;
            Exception = exception;
        }

        protected void SetStatusCode(HttpStatusCode status)
        {
            Status = status;
        }
    }

    public abstract class ActionResponseBase<T> : IActionResponse<T> where T : class
    {
        public HttpStatusCode Status { get; private set; }
        public T Content { get; private set; }
        public string ErrorMessage { get; private set; }
        public Exception Exception { get; private set; }

        protected ActionResponseBase(T content)
        {
            Content = content;
        }

        protected ActionResponseBase(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        protected ActionResponseBase(string errorMessage, T defaultContent)
        {
            ErrorMessage = errorMessage;
            Content = defaultContent;
        }

        protected ActionResponseBase(string errorMessage, Exception exception)
        {
            ErrorMessage = errorMessage;
            Exception = exception;
        }

        protected ActionResponseBase(string errorMessage, T content, Exception exception)
        {
            ErrorMessage = errorMessage;
            Content = content;
            Exception = exception;
        }

        protected void SetStatusCode(HttpStatusCode status)
        {
            Status = status;
        }
    }
}
