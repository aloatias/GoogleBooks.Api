using GoogleBooks.Api.Dtos.Output.Exceptions;

namespace GoogleBooks.Api.Dtos.Output
{
    public class ResultBase
    {
        public StatusEnum Status { get; private set; }

        public ErrorBase Error { get; private set; }

        protected ResultBase(StatusEnum status)
        {
            Status = status;
        }

        protected ResultBase(ErrorBase error, StatusEnum status)
        {
            Error = error;
            Status = status;
        }
    }
}
