namespace GoogleBooks.Client.Interfaces
{
    public interface IUrlFactory
    {
        string GetSearchDefaultsBooksUrl(string keywords);
    }
}
