using GoogleBooks.Client.Interfaces;
using Xunit;

namespace GoogleBooks.Client.Tests
{
    public class GetBooksTests : TestFactory
    {
        private readonly IGoogleBooksClientService _googleBooksClientService;

        public GetBooksTests()
        {
            _googleBooksClientService = CreateGoogleBooksClientService();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
