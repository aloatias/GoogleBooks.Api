using GoogleBooks.Client.Interfaces;
using System.Linq;
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
            int expectedItemsNumber = 10;
            int pageNumber = 0;

            // Act
            var actualResult = await _googleBooksClientService.GetBooksCatalogAsync(keyword, expectedItemsNumber, pageNumber);

            // Test
            Assert.NotNull(actualResult);
            Assert.Equal(expectedItemsNumber, actualResult.Items.Count()); ;
            Assert.NotNull(actualResult.Kind);
        }

        [Fact]
        public async void Should_GetBookDetailsById()
        {
            // Prepare
            string keyword = "tennis";
            int expectedItemsNumber = 40;
            int pageNumber = 0;


            // Act
            var actualGetBooksResult = await _googleBooksClientService.GetBooksCatalogAsync(keyword, expectedItemsNumber, pageNumber);
            var actualGetBookDetailsResult = await _googleBooksClientService.GetBookDetailsAsync(actualGetBooksResult.Items[0].Id);

            // Test
            Assert.NotNull(actualGetBookDetailsResult);
            Assert.Equal(expectedItemsNumber, actualGetBooksResult.Items.Count());
        }
    }
}
