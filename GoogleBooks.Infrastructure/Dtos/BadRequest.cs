using System;
using System.Net;

namespace GoogleBooks.Infrastructure.Dtos
{
    public class BadRequest : ActionResponseBase
    {
        public BadRequest(string errorMessage) : base(errorMessage)
        {
            SetStatusCode(HttpStatusCode.BadRequest);
        }
    }

    public class BadRequest<T> : ActionResponseBase<T> where T : class
    {
        public BadRequest(string errorMessage) : base(errorMessage)
        {
            SetStatusCode(HttpStatusCode.BadRequest);
        }

        public BadRequest(string errorMessage, T content) : base(errorMessage, content)
        {
            SetStatusCode(HttpStatusCode.BadRequest);
        }
    }
}
