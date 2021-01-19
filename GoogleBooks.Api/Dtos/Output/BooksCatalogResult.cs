namespace GoogleBooks.Api.Dtos.Output
{
    public class BooksCatalogResult
    {
        public PagingCatalogResult PagingInfo { get; private set; }
        public BooksCatalog BooksCatalog { get; private set; }

        public BooksCatalogResult(BooksCatalogSearchResult booksCatalogSearchResult)
        {
            BooksCatalog = booksCatalogSearchResult.BooksCatalog;
            PagingInfo = booksCatalogSearchResult.PagingInfoResult;
        }
    }
}
