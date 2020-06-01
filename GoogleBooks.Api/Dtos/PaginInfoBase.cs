namespace GoogleBooks.Api.Dtos
{
    public abstract class PagingInfoBase
    {
        public string Keywords { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}