using System.Net;

namespace GoogleBooks.Infrastructure.Dtos
{
    public class Ok : ActionResponseBase
    {
        public Ok()
        {
            SetStatusCode(HttpStatusCode.OK);
        }
    }

    public class Ok<T> : ActionResponseBase<T> where T : class
    {
        public Ok(T content) : base(content)
        {
            SetStatusCode(HttpStatusCode.OK);
        }
    }
}
