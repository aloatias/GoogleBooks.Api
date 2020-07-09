namespace GoogleBooks.Api.Dtos.Output
{
    public class PagingCatalogResult : PagingCatalogBase
    {
        public int TotalItems { get; private set; }

        public PagingCatalogResult
        (
            string keywords,
            int pageNumber,
            int pageSize,
            int totalItems)
        {
            Keywords = keywords;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
    }
}