namespace GoogleBooks.Api.Domain
{
    public interface IDomainFactory
    {
        Book CreateBook(string bookId);

        BooksCatalog CreateBooksCatalog(string keywords, int pageNumber, int pageSize);
    }
}