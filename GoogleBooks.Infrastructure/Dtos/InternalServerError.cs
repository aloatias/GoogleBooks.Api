using System;
using System.Net;

namespace GoogleBooks.Infrastructure.Dtos
{
    public class InternalServerError : ActionResponseBase
    {
        public InternalServerError(string errorMessage) : base(errorMessage)
        {
            SetStatusCode(HttpStatusCode.InternalServerError);
        }

        public InternalServerError(string errorMessage, Exception exception) : base(errorMessage, exception)
        {
            SetStatusCode(HttpStatusCode.InternalServerError);
        }
    }

    public class InternalServerError<T> : ActionResponseBase<T> where T : class
    {
        public InternalServerError(string errorMessage, Exception exception) : base(errorMessage, exception)
        {
            SetStatusCode(HttpStatusCode.InternalServerError);
        }

        public InternalServerError(string errorMessage, Exception exception, T content) : base(errorMessage, content, exception)
        {
            SetStatusCode(HttpStatusCode.InternalServerError);
        }
    }
}
