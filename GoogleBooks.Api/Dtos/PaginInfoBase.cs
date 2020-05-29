namespace GoogleBooks.Api.Dtos
{
    public class PaginInfoBase
    {
        public string Keywords { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}