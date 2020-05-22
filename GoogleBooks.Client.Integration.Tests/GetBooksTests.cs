using GoogleBooks.Client.Interfaces;
using Xunit;

namespace GoogleBooks.Client.Integration.Tests
{
    public class GetBooksTests : TestFactory
    {
        private readonly IGoogleBooksClientService _googleBooksClientService;

        public GetBooksTests()
        {
            _googleBooksClientService = CreateGoogleBooksClientService();
        }

        [Fact]
        public async void Should_GetBooksByKeyword()
        {
            // Prepare
            string keyword = "tennis";

            // Act
            var actualResult = await _googleBooksClientService.GetBooksByKeyword(keyword);

            // Test
            Assert.NotNull(actualResult);
            Assert.True(actualResult.TotalItems > 0);
            Assert.NotNull(actualResult.Kind);
        }
    }
}
