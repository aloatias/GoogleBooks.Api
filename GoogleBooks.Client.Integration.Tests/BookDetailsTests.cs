using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Infrastructure.Dtos;
using NFluent;
using System;
using System.Threading.Tasks;
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
            var expectedResult = new Ok<GoogleBookDetailsFull>(new GoogleBookDetailsFull());

            // Act
            var actualResult = await _googleBooksClientService.GetBookDetailsAsync(bookId);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(bookId).Equals(actualResult.Content.Id);
            Check.That(actualResult).IsInstanceOf<Ok<GoogleBookDetailsFull>>();
            Check.That(actualResult.Content).IsNotNull();
        }

        [Fact(DisplayName = "Should throw exception when no Id is matched")]
        public async Task Should_ThrowExceptionWhenNoMatchingId()
        {
            // Prepare
            var bookId = "NoMatchingId";
            var expectedException = new Exception("Response status code does not indicate success: 404 (Not Found).");
            var expectedResult = new InternalServerError<GoogleBookDetailsFull>(expectedException.Message, expectedException);

            // Act
            var actualResult = await _googleBooksClientService.GetBookDetailsAsync(bookId);

            // Test
            Check.That(expectedResult.ErrorMessage).Equals(actualResult.ErrorMessage);
            Check.That(expectedResult).IsInstanceOf<InternalServerError<GoogleBookDetailsFull>>();
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.Content).IsNull();
        }
    }
}
