using GoogleBooks.Api.Dtos.Output;

namespace GoogleBooks.Api.Dtos
{
    public class BooksCatalogSearchResult
    {
        public PagingInfoResult PagingInfoResult { get; set; }

        public BooksCatalog BooksCatalog { get; set; }
    }
}
