namespace GoogleBooks.Client.Interfaces
{
    public interface IUrlFactory
    {
        string GetBookDetailsUrl(string bookId);

        string GetDefaultsBooksUrl(string keywords);
    }
}
