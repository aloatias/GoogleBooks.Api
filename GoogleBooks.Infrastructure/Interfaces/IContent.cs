namespace GoogleBooks.Infrastructure.Interfaces
{
    public interface IContent<T> where T : class
    {
        T Content { get; }
    }
}