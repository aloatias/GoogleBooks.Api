namespace GoogleBooks.Api.Domain
{
    public class DomainFactory : IDomainFactory
    {
        public Book CreateBook(string bookId)
        {
            return new Book(bookId);
        }

        public BooksCatalog CreateBooksCatalog(string keywords, int pageNumber, int pageSize)
        {
            return new BooksCatalog(keywords, pageNumber, pageSize);
        }
    }
}
