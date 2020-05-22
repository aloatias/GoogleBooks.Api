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
        public async void Should_GetBooksByKeyword_()
        {
            // Prepare
            string keyword = "tennis";

            // Act
            var getBooksActualResult = await _googleBooksClientService.GetBooksByKeyword(keyword);

            // Test
            Assert.NotNull(getBooksActualResult);
            Assert.True(getBooksActualResult.TotalItems > 0);
            Assert.NotNull(getBooksActualResult.Kind);
        }
    }
}
