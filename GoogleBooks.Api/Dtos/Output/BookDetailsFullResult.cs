using GoogleBooks.Api.Dtos.Output.Exceptions;

namespace GoogleBooks.Api.Dtos.Output
{
    public class BookDetailsFullResult : ResultBase
    {
        public BookDetailsFull BookDetails { get; private set; }

        public BookDetailsFullResult(BookDetailsFull bookDetailsFull, StatusEnum status) : base(status)
        {
            BookDetails = bookDetailsFull;
        }

        public BookDetailsFullResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}
