using GoogleBooks.Api.Dtos.Output.Exceptions;

namespace GoogleBooks.Api.Dtos.Output
{
    public class BooksByKeywordResult : ResultBase
    {
        public int TotalResults { get; private set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public BooksCatalog BooksCatalog { get; private set; }

        public BooksByKeywordResult(BooksCatalog booksCatalog, StatusEnum status) : base(status)
        {
            BooksCatalog = booksCatalog;
        }

        public BooksByKeywordResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}
