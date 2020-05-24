namespace GoogleBooks.Client.Interfaces
{
    public interface IUrlFactory
    {
        string Url { get; }

        void SetBookDetailsUrl(string bookId);

        void SetBookCatalogSearchUrl(string keywords);

        void SetMaxResultsParameter(int maxResults);
    }
}
