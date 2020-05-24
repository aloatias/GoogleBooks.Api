using GoogleBooks.Api.Dtos.Output.Exceptions;

namespace GoogleBooks.Api.Dtos.Output
{
    public class BooksByKeywordsResult : ResultBase
    {
        public PagingInfo PagingInfo { get; private set; }

        public BooksCatalog BooksCatalog { get; private set; }

        public BooksByKeywordsResult(BooksCatalog booksCatalog, PagingInfo pagingInfo, StatusEnum status) : base(status)
        {
            BooksCatalog = booksCatalog;
            PagingInfo = pagingInfo;
        }

        public BooksByKeywordsResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}
