namespace GoogleBooks.Api.Dtos
{
    public abstract class PagingInfoBase
    {
        public string Keywords { get; private set; }

        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        protected PagingInfoBase(string keywords, int pageNumber, int pageSize)
        {
            Keywords = keywords;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}