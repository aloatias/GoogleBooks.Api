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
        public NoContent(string errorMessage) : base(errorMessage)
        {
            SetStatusCode(HttpStatusCode.NoContent);
        }

        public NoContent(string errorMessage, T defaultContent) : base(errorMessage, defaultContent)
        {
            SetStatusCode(HttpStatusCode.NoContent);
        }
    }

    public class NoContent<T, S> : ActionResponseBase<T> where T : class where S : class
    {
        public S SearchCriteria { get; private set; }

        public NoContent(string errorMessage, S searchCriteria) : base(errorMessage)
        {
            SearchCriteria = searchCriteria;
        }
    }
}
