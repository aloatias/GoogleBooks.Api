using GoogleBooks.Client.Interfaces;
using GoogleBooks.Client.Services;

namespace GoogleBooks.Client.Tests
{
    public class TestFactory
    {
        protected IGoogleBooksClientService CreateGoogleBooksClientService()
        {
            return new GoogleBooksClientService();
        }
    }
}