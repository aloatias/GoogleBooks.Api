using System;
using System.Net;

namespace GoogleBooks.Api.Dtos
{
    public abstract class ActionResponseBase : IActionResponse
    {
        public HttpStatusCode Status { get; protected set; }
        public string ErrorMessage { get; private set; }
        public Exception Exception { get; private set; }

        protected ActionResponseBase()
        {
        }

        protected ActionResponseBase(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public ActionResponseBase(string errorMessage, Exception exception)
        {
            ErrorMessage = errorMessage;
            Exception = exception;
        }
    }

    public abstract class ActionResponseBase<T> : IActionResponse<T> where T : class
    {
        public HttpStatusCode Status { get; protected set; }
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
    }

    public class Ok : ActionResponseBase
    {
        public Ok()
        {
            Status = HttpStatusCode.OK;
        }
    }

    public class Ok<T> : ActionResponseBase<T> where T : class
    {
        public Ok(T content) : base(content)
        {
            Status = HttpStatusCode.OK;
        }
    }

    public class NotFound : ActionResponseBase
    {
        public NotFound(string errorMessage) : base(errorMessage)
        {
            Status = HttpStatusCode.NotFound;
        }
    }

    public class NotFound<T> : ActionResponseBase<T> where T : class
    {
        public NotFound(string errorMessage) : base(errorMessage)
        {
        }

        public NotFound(string errorMessage, T defaultContent) : base(errorMessage, defaultContent)
        {
            Status = HttpStatusCode.NotFound;
        }
    }

    public class NoContent : ActionResponseBase
    {
        public NoContent(string errorMessage) : base(errorMessage)
        {
            Status = HttpStatusCode.NoContent;
        }
    }

    public class NoContent<T> : ActionResponseBase<T> where T : class
    {
        public NoContent(T defaultContent, string errorMessage) : base(errorMessage, defaultContent)
        {
            Status = HttpStatusCode.NoContent;
        }
    }

    public class BadRequest : ActionResponseBase
    {
        public BadRequest(string errorMessage) : base(errorMessage)
        {
            Status = HttpStatusCode.BadRequest;
        }
    }

    public class BadRequest<T> : ActionResponseBase<T> where T : class
    {
        public BadRequest(string errorMessage) : base(errorMessage)
        {
            Status = HttpStatusCode.BadRequest;
        }

        public BadRequest(string errorMessage, T content) : base(errorMessage, content)
        {
            Status = HttpStatusCode.BadRequest;
        }
    }

    public class InternalServerError : ActionResponseBase
    {
        public InternalServerError(string errorMessage) : base(errorMessage)
        {
            Status = HttpStatusCode.InternalServerError;
        }

        public InternalServerError(string errorMessage, Exception exception) : base(errorMessage, exception)
        {
            Status = HttpStatusCode.InternalServerError;
        }
    }

    public class InternalServerError<T> : ActionResponseBase<T> where T : class
    {
        public InternalServerError(string errorMessage, Exception exception) : base(errorMessage, exception)
        {
            Status = HttpStatusCode.InternalServerError;
        }

        public InternalServerError(string errorMessage, Exception exception, T content) : base(errorMessage, content, exception)
        {
            Status = HttpStatusCode.InternalServerError;
        }
    }
}
