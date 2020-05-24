namespace GoogleBooks.Api.Dtos
{
    public class BooksCatalogSearch
    {
        public string Keywords { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
