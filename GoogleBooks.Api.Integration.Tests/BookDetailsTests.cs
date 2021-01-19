using AutoMapper;
using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Api.Services;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Infrastructure.Dtos;
using Microsoft.Extensions.Logging;
using Moq;
using NFluent;
using System;
using Xunit;

namespace GoogleBooks.Api.Integration.Tests
{
    public class BookDetailsTests : TestFactory
    {
        private IBooksService _bookService;
        private Mock<IGoogleBooksClientService> _mockedGoogleClientService;
        private Mock<IMapper> _mockedMapperService;
        private readonly ILogger<BooksService> _logger;

        public BookDetailsTests()
        {
            _mockedGoogleClientService = MockService<IGoogleBooksClientService>();
            _mockedMapperService = MockService<IMapper>();
            _logger = CreateLogger<BooksService>();
        }

        [Fact(DisplayName = "Get book details for existing Id")]
        public async void Should_GetBookDetailsForExistingId()
        {
            // Prepare
            var bookId = "W7Y7CwAAQBAJ";

            var googleClientResult = new Ok<GoogleBookDetailsFull>(
                new GoogleBookDetailsFull
                {
                    Id = bookId,
                    Etag = "Test Etag",
                    AccessInfo = new AccessInfo
                    {
                        Country = "Test Country",
                        AccessViewStatus = "Test AccessViewStatus",
                        QuoteSharingAllowed = "Test AccessViewStatus",
                        TextToSpeechPermission = "Test TextToSpeechPermission",
                        Viewability = "Test Viewability",
                        WebReaderLink = "Test WebReaderLink"
                    },
                    Kind = "Test Kind",
                    SelfLink = "Test SelfLink",
                    VolumeInfo = new VolumeInfoFull
                    {
                        Authors = new string[] { "Test Author" },
                        CanonicalVolumeLink = "Test CanonicalVolumeLink",
                        Description = "Test Description",
                        Categories = new string[] { "Test Category" },
                        InfoLink = "Test InfoLink",
                        Language = "Test Languge",
                        PageCount = 100,
                    },
                    SaleInfo = new SaleInfoFull
                    {
                        ListPrice = new ListPrice
                        {
                            Amount = 25,
                            CurrencyCode = "EUR"
                        }
                    }
                }
            );

            _mockedGoogleClientService.Setup(s => s.GetBookDetailsAsync(bookId)).ReturnsAsync(googleClientResult);

            var mapperServiceResult = new IndividualBookDetails
            {
                Id = bookId,
                Etag = "Test Etag",
                Country = "Test Country",
                AccessViewStatus = "Test AccessViewStatus",
                QuoteSharingAllowed = "Test AccessViewStatus",
                Viewability = "Test Viewability",
                WebReaderLink = "Test WebReaderLink",
                Authors = new string[] { "Test Author" },
                CanonicalVolumeLink = "Test CanonicalVolumeLink",
                Description = "Test Description",
                Categories = new string[] { "Test Category" },
                InfoLink = "Test InfoLink",
                Language = "Test Languge",
                PageCount = 100,
                Price = 25,
                CurrencyCode = "EUR"
            };

            _mockedMapperService
                .Setup(s => s.Map<IndividualBookDetails>(googleClientResult))
                .Returns(mapperServiceResult);

            var bookEntryParameter = new Book(bookId);
            var expectedResult = new Ok<IndividualBookDetails>(mapperServiceResult);

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(bookEntryParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.Content).Equals(actualResult.Content);
        }

        [Fact(DisplayName = "Should respond with an invalid parameter exception because of null 'Book' argument")]
        public async void Should_RespondInvalidParameterException()
        {
            // Prepare
            Book book = null;
            var expectedResult = new BadRequest("Bad request");

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(book);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.ErrorMessage).Equals(actualResult.ErrorMessage);
        }

        [Fact(DisplayName = "Should respond with a not found exception because the Id wasn't found on google client")]
        public async void Should_RespondIdNotFoundException()
        {
            // Prepare
            var book = new Book("unexistingId");
            var expectedResult = new NotFound<IndividualBookDetails>("The book Id doesn't exist");

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(book);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.ErrorMessage).Equals(actualResult.ErrorMessage);
        }

        [Fact(DisplayName = "Should respond with an internal server exception because the google client failed")]
        public async void Should_RespondInternalServerExceptionWhenFailingOnGoogleClient()
        {
            // Prepare
            var book = new Book("W7Y7CwAAQBAJ");
            var expectedResult = new InternalServerError("An error occured");

            _mockedGoogleClientService
                .Setup(s => s.GetBookDetailsAsync(book.Id))
                .Throws(new Exception("Google client unexpected exception"));
            
            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(book);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.ErrorMessage).Equals(actualResult.ErrorMessage);
        }

        [Fact(DisplayName = "Should respond with an internal server exception because the mapper service failed")]
        public async void Should_RespondInternalServerExceptionWhenFailingOnMapperService()
        {
            // Prepare
            var book = new Book("W7Y7CwAAQBAJ");
            var expectedResult = new InternalServerError("An error occured");

            var googleClientResult = new InternalServerError<GoogleBookDetailsFull>(
                expectedResult.ErrorMessage,
                new Exception()
            );
            
            _mockedGoogleClientService.Setup(s => s.GetBookDetailsAsync(book.Id)).ReturnsAsync(googleClientResult);

            _mockedMapperService
                .Setup(s => s.Map<IndividualBookDetails>(googleClientResult))
                .Throws(new Exception("Mapper service unexpected exception"));
            
            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(book);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.ErrorMessage).Equals(actualResult.ErrorMessage);
        }
    }
}
