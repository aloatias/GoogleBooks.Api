using System.Net;

namespace GoogleBooks.Infrastructure.Dtos
{
    public class NotFound : ActionResponseBase
    {
        public NotFound(string errorMessage) : base(errorMessage)
        {
            SetStatusCode(HttpStatusCode.NotFound);
        }
    }

    public class NotFound<T> : ActionResponseBase<T> where T : class
    {
        public NotFound(string errorMessage) : base(errorMessage)
        {
            SetStatusCode(HttpStatusCode.NotFound);
        }

        public NotFound(string errorMessage, T defaultContent) : base(errorMessage, defaultContent)
        {
            SetStatusCode(HttpStatusCode.NotFound);
        }
    }
}
