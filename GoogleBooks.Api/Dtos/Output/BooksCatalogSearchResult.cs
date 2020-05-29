using GoogleBooks.Api.Dtos.Output;

namespace GoogleBooks.Api.Dtos
{
    public class BooksCatalogSearchResult
    {
        public PagingInfoResult PagingInfoResult { get; private set; }

        public BooksCatalog BooksCatalog { get; private set; }

        public BooksCatalogSearchResult(PagingInfoResult pagingInfoResult, BooksCatalog booksCatalog)
        {
            PagingInfoResult = pagingInfoResult;
            BooksCatalog = booksCatalog;
        }
    }
}
