using GoogleBooks.Api.Dtos.Output.Exceptions;

namespace GoogleBooks.Api.Dtos.Output
{
    public class IndividualBookDetailsResult : ResultBase
    {
        public IndividualBookDetails IndividualBookDetails { get; private set; }

        public IndividualBookDetailsResult(IndividualBookDetails individualBookDetails, StatusEnum status) : base(status)
        {
            IndividualBookDetails = individualBookDetails;
        }

        public IndividualBookDetailsResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}
