using GoogleBooks.Api.Dtos.Output;

namespace GoogleBooks.Api.Dtos
{
    public class BooksCatalogSearchResult
    {
        public PagingCatalogResult PagingInfoResult { get; private set; }

        public BooksCatalog BooksCatalog { get; private set; }

        public BooksCatalogSearchResult(PagingCatalogResult pagingInfoResult, BooksCatalog booksCatalog)
        {
            PagingInfoResult = pagingInfoResult;
            BooksCatalog = booksCatalog;
        }
    }
}
