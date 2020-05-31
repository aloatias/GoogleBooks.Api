namespace GoogleBooks.Api.Dtos
{
    public class PagingInfoResult : PagingInfoBase
    {
        public int TotalItems { get; private set; }

        public PagingInfoResult(string keywords, int pageNumber, int pageSize, int totalItems) : base(keywords, pageNumber, pageSize)
        {
            TotalItems = totalItems;
        }
    }
}