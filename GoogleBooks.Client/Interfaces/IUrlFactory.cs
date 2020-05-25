namespace GoogleBooks.Client.Interfaces
{
    public interface IUrlFactory
    {
        string Url { get; }

        void SetBookDetailsUrl(string bookId);

        void SetBooksCatalogUrl(string keywords);

        void SetMaxResultsParameter(int pageSize);

        void SetStartIndexParameter(int startIndex);
    }
}
