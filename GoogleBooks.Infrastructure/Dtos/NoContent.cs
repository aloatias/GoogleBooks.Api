using System.Net;

namespace GoogleBooks.Infrastructure.Dtos
{
    public class NoContent : ActionResponseBase
    {
        public NoContent(string errorMessage) : base(errorMessage)
        {
            SetStatusCode(HttpStatusCode.NoContent);
        }
    }

    public class NoContent<T> : ActionResponseBase<T> where T : class
    {
        public NoContent(T defaultContent, string errorMessage) : base(errorMessage, defaultContent)
        {
            SetStatusCode(HttpStatusCode.NoContent);
        }
    }
}
