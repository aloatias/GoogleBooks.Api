using GoogleBooks.Api.Dtos.Output.Exceptions;

namespace GoogleBooks.Api.Dtos.Output
{
    public class BookDetailsFullResult : ResultBase
    {
        public IndividualBookDetails BookDetails { get; private set; }

        public BookDetailsFullResult(IndividualBookDetails bookDetailsFull, StatusEnum status) : base(status)
        {
            BookDetails = bookDetailsFull;
        }

        public BookDetailsFullResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}
