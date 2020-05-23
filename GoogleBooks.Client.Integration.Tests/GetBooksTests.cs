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
            var actualResult = await _googleBooksClientService.GetBooksByKeywordAsync(keyword);

            // Test
            Assert.NotNull(actualResult);
            Assert.True(actualResult.TotalItems > 0);
            Assert.NotNull(actualResult.Kind);
        }

        [Fact]
        public async void Should_GetBookDetailsById()
        {
            // Prepare
            string keyword = "tennis";

            // Act
            var actualGetBooksResult = await _googleBooksClientService.GetBooksByKeywordAsync(keyword);
            var actualGetBookDetailsResult = await _googleBooksClientService.GetBookDetailsByIdAsync(actualGetBooksResult.Items[0].Id);

            // Test
            Assert.NotNull(actualGetBookDetailsResult);
        }
    }
}
