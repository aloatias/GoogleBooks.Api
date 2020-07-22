using GoogleBooks.Client.Interfaces;
using NFluent;
using System;
using Xunit;

namespace GoogleBooks.Client.Integration.Tests
{
    public class BookDetailsTests : TestFactory
    {
        private readonly IGoogleBooksClientService _googleBooksClientService;
     
        public BookDetailsTests()
        {
            _googleBooksClientService = CreateGoogleBooksClientService();
        }

        [Fact(DisplayName = "Should get book details when matching an Id")]
        public async void Should_GetBookDetailsWhenMatchingId()
        {
            // Prepare
            var bookId = "W7Y7CwAAQBAJ";

            // Act
            var actualResult = await _googleBooksClientService.GetBookDetailsAsync(bookId);

            // Test
            Check.That(actualResult).IsNotNull();
            Check.That(bookId).Equals(actualResult.Id);
        }

        [Fact(DisplayName = "Should throw exception when no Id is matched")]
        public void Should_ThrowExceptionWhenNoMatchingId()
        {
            // Prepare
            var bookId = "NoMatchingId";

            // Test
            Check.ThatCode(async () => await _googleBooksClientService.GetBookDetailsAsync(bookId)).Throws<Exception>();
        }
    }
}
