namespace GoogleBooks.Client.Interfaces
{
    public interface IUrlFactory
    {
        string Url { get; }

        void SetBookDetailsUrl(string bookId);

        void SetDefaultsBooksUrl(string keywords);

        void SetMaxResults(int maxResults);
    }
}
