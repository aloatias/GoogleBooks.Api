using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Helpers;

namespace GoogleBooks.Api.Domain
{
    public class BooksCatalog
    {
        private const int minimalKeywordsLength = 2;

        private const int minimalPageNumber = 0;

        private const int minimalPageSize = 1;

        public string Keywords { get; private set; }

        public int PageNumber { get; private set; }
        
        public int PageSize { get; private set; }

        public BooksCatalog(string keywords, int pageNumber, int pageSize)
        {
            if (keywords.Length < minimalKeywordsLength)
            {
                throw new InvalidBooksCatalogException(ExceptionMessages.InvalidKeyword);
            }

            if (pageNumber < minimalPageNumber)
            {
                throw new InvalidBooksCatalogException(ExceptionMessages.InvalidPageNumber);
            }

            if (pageSize < minimalPageSize)
            {
                throw new InvalidBooksCatalogException(ExceptionMessages.InvalidPageSize);
            }

            Keywords = keywords;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}