using System.Collections.Generic;

namespace GoogleBooks.Api.Dtos.Output
{
    public class BooksCatalog
    {
        public string Kind { get; private set; }

        public List<BookDetailsForCatalog> BookDetails { get; private set; }

        public BooksCatalog(string kind, List<BookDetailsForCatalog> bookDetails)
        {
            Kind = kind;
            BookDetails = bookDetails;
        }
    }
}