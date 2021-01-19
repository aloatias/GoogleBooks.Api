using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Helpers;

namespace GoogleBooks.Api.Domain
{
    public class BooksCatalog
    {
        private const int _minimalKeywordsLength = 2;
        private const int _minimalPageNumber = 0;
        private const int _minimalPageSize = 1;

        public string Keywords { get; private set; }

        public int PageNumber { get; private set; }
        
        public int PageSize { get; private set; }

        public BooksCatalog(string keywords, int pageNumber, int pageSize)
        {
            if (keywords.Length < _minimalKeywordsLength)
            {
                throw new InvalidBooksCatalogException(ExceptionMessages.InvalidKeyword);
            }

            if (pageNumber < _minimalPageNumber)
            {
                throw new InvalidBooksCatalogException(ExceptionMessages.InvalidPageNumber);
            }

            if (pageSize < _minimalPageSize)
            {
                throw new InvalidBooksCatalogException(ExceptionMessages.InvalidPageSize);
            }

            Keywords = keywords;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}