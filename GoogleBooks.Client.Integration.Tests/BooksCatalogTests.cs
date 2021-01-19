using GoogleBooks.Client.Interfaces;
using NFluent;
using System.Linq;
using Xunit;

namespace GoogleBooks.Client.Integration.Tests
{
    public class BooksCatalogTests : TestFactory
    {
        private readonly IGoogleBooksClientService _googleBooksClientService;

        public BooksCatalogTests()
        {
            _googleBooksClientService = CreateGoogleBooksClientService();
        }

        [Fact(DisplayName = "Should get books catalog when matching keywords")]
        public async void Should_GetBooksCatalogWhenMatchingKeywords()
        {
            // Prepare
            string keyword = "tennis";
            int expectedItemsCount = 10;
            int pageNumber = 0;
            string expectedKind = "books#volumes";

            // Act
            var actualResult = await _googleBooksClientService.GetBooksCatalogAsync(keyword, expectedItemsCount, pageNumber);
            
            // Test
            Check.That(actualResult.Content.Kind).Equals(expectedKind);
            Check.That(actualResult.Content.Items.Count()).Equals(expectedItemsCount);
        }

        [Fact(DisplayName = "Should get null books catalog when not matching keywords")]
        public async void Should_GetNullResponseWhenNoMatchingKeywords()
        {
            // Prepare
            string keyword = "ThisShouldMatchNothingThisShouldMatchNothingThisShouldMatchNothingThisShouldMatchNothingThisShouldMatchNothing";
            int expectedItemsCount = 10;
            int pageNumber = 0;
            string expectedKind = "books#volumes";

            // Act
            var actualResult = await _googleBooksClientService.GetBooksCatalogAsync(keyword, expectedItemsCount, pageNumber);

            // Test
            Check.That(actualResult.Content.Kind).Equals(expectedKind);
            Check.That(actualResult.Content.Items).IsNull();
            Check.That(actualResult.Content.TotalItems).Equals(0);
        }
    }
}
