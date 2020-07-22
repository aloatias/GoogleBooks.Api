using AutoMapper;
using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Helpers;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Api.Services;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
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

            var googleClientResult = new GoogleBookDetailsFull
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
            };

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
            var expectedResult = new IndividualBookDetailsResult(mapperServiceResult, StatusEnum.Ok);

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(bookEntryParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.IndividualBookDetails).Equals(actualResult.IndividualBookDetails);
        }

        [Fact(DisplayName = "Should respond with an invalid parameter exception because of null 'Book' argument")]
        public async void Should_RespondInvalidParameterException()
        {
            // Prepare
            Book book = null;
            var expectedResult = new IndividualBookDetailsResult
            (
                new InvalidBookException(ExceptionMessages.NullArgument),
                StatusEnum.InvalidParamater
            );

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(book);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.Error.Data).Equals(actualResult.Error.Data);
            Check.That(expectedResult.Error.Message).Equals(actualResult.Error.Message);
        }

        [Fact(DisplayName = "Should respond with a not found exception because the Id wasn't found on google client")]
        public async void Should_RespondIdNotFoundException()
        {
            // Prepare
            var book = new Book("unexistingId");
            var expectedResult = new IndividualBookDetailsResult
            (
                new NotFoundException(ExceptionMessages.GetNotFoundMessage(book.Id)),
                StatusEnum.NotFound
            );

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(book);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.Error.Data).Equals(actualResult.Error.Data);
            Check.That(expectedResult.Error.Message).Equals(actualResult.Error.Message);
        }

        [Fact(DisplayName = "Should respond with an internal server exception because the google client failed")]
        public async void Should_RespondInternalServerExceptionWhenFailingOnGoogleClient()
        {
            // Prepare
            var book = new Book("W7Y7CwAAQBAJ");
            var expectedResult = new IndividualBookDetailsResult
            (
                new InternalServerException("Google client unexpected exception"), 
                StatusEnum.InternalError
            );

            _mockedGoogleClientService
                .Setup(s => s.GetBookDetailsAsync(book.Id))
                .Throws(new Exception("Google client unexpected exception"));
            
            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(book);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(actualResult.Error).IsInstanceOf<InternalServerException>();
            Check.That(expectedResult.Error.Message).Equals(actualResult.Error.Message);
        }

        [Fact(DisplayName = "Should respond with an internal server exception because the mapper service failed")]
        public async void Should_RespondInternalServerExceptionWhenFailingOnMapperService()
        {
            // Prepare
            var book = new Book("W7Y7CwAAQBAJ");
            var expectedResult = new IndividualBookDetailsResult
            (
                new InternalServerException("Mapper service unexpected exception"),
                StatusEnum.InternalError
            );

            var googleClientResult = new GoogleBookDetailsFull();
            _mockedGoogleClientService.Setup(s => s.GetBookDetailsAsync(book.Id)).ReturnsAsync(googleClientResult);

            _mockedMapperService
                .Setup(s => s.Map<IndividualBookDetails>(googleClientResult))
                .Throws(new Exception("Mapper service unexpected exception"));
            
            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBookDetailsAsync(book);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(actualResult.Error).IsInstanceOf<InternalServerException>();
            Check.That(expectedResult.Error.Message).Equals(actualResult.Error.Message);
        }
    }
}
