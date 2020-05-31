namespace GoogleBooks.Api.Dtos
{
    public class BooksCatalogSearch : PagingInfoBase
    {
        public BooksCatalogSearch(string keywords, int pageNumber, int pageSize) : base(keywords, pageNumber, pageSize)
        {
        }
    }
}