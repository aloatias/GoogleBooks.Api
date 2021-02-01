using AutoMapper;
using GoogleBooks.Api.Dtos;
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
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GoogleBooks.Api.Integration.Tests
{
    public class BooksCatalogTests : TestBase
    {
        private IBooksService _bookService;
        private Mock<IGoogleBooksClientService> _mockedGoogleClientService;
        private Mock<IMapper> _mockedMapperService;
        private readonly ILogger<BooksService> _logger;

        public BooksCatalogTests()
        {
            _mockedGoogleClientService = MockService<IGoogleBooksClientService>();
            _mockedMapperService = MockService<IMapper>();
            _logger = CreateLogger<BooksService>();
        }

        [Fact(DisplayName = "Should get 5 books from catalog")]
        public async Task Should_GetFiveBooksFromCatalog()
        {
            // Prepare
            var keywords = "Test Keywords";
            var pageSize = 100;
            var pageNumber = 0;

            var kind = "Test Kind";
            var expectedResult = new Ok<GoogleBooksCatalog>(new GoogleBooksCatalog
            {
                Kind = kind,
                TotalItems = 5,
                Items = new GoogleBookDetailsLite[]
                {
                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 1",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 1",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 1,
                        },
                    },

                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 2",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 2",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 2,
                        },
                    },

                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 3",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 3",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 3,
                        },
                    },

                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 4",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 4",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 4,
                        },
                    },

                    new GoogleBookDetailsLite
                    {
                        AccessInfo = new AccessInfo
                        {
                            Country = "Test Country 5",
                            AccessViewStatus = "Test AccessViewStatus",
                            QuoteSharingAllowed = "Test AccessViewStatus",
                            TextToSpeechPermission = "Test TextToSpeechPermission",
                            Viewability = "Test Viewability",
                            WebReaderLink = "Test WebReaderLink"
                        },
                        Kind = "Test Kind",
                        SelfLink = "Test SelfLink",
                        SaleInfo = new SaleInfoFull
                        {
                            ListPrice = new ListPrice
                            {
                                Amount = 25,
                                CurrencyCode = "EUR"
                            }
                        },
                        VolumeInfo = new VolumeInfoLite
                        {
                            Authors = new string[] { "Test Author" },
                            CanonicalVolumeLink = "Test CanonicalVolumeLink",
                            Description = "Test Description 5",
                            Categories = new string[] { "Test Category" },
                            InfoLink = "Test InfoLink",
                            Language = "Test Languge",
                            PageCount = 5,
                        },
                    }
                }
            });
            _mockedGoogleClientService.Setup(s => s.GetBooksCatalogAsync(keywords, pageSize, pageNumber)).ReturnsAsync(expectedResult);

            //var mapperServiceResult = new List<BookDetailsForCatalog>
            //{
            //    new BookDetailsForCatalog
            //    {
            //        Country = "Test Country 1",
            //        AccessViewStatus = "Test AccessViewStatus",
            //        QuoteSharingAllowed = "Test AccessViewStatus",
            //        TextToSpeechPermission = "Test TextToSpeechPermission",
            //        WebReaderLink = "Test WebReaderLink",
            //        Kind = "Test Kind",
            //        SelfLink = "Test SelfLink",
            //        Authors = new string[] { "Test Author" },
            //        CanonicalVolumeLink = "Test CanonicalVolumeLink",
            //        Description = "Test Description 1",
            //        Categories = new string[] { "Test Category" },
            //        InfoLink = "Test InfoLink",
            //        Language = "Test Languge",
            //        PageCount = 1
            //    },

            //    new BookDetailsForCatalog
            //    {
            //        Country = "Test Country 2",
            //        AccessViewStatus = "Test AccessViewStatus",
            //        QuoteSharingAllowed = "Test AccessViewStatus",
            //        TextToSpeechPermission = "Test TextToSpeechPermission",
            //        WebReaderLink = "Test WebReaderLink",
            //        Kind = "Test Kind",
            //        SelfLink = "Test SelfLink",
            //        Authors = new string[] { "Test Author" },
            //        CanonicalVolumeLink = "Test CanonicalVolumeLink",
            //        Description = "Test Description 2",
            //        Categories = new string[] { "Test Category" },
            //        InfoLink = "Test InfoLink",
            //        Language = "Test Languge",
            //        PageCount = 2
            //    },

            //    new BookDetailsForCatalog
            //    {
            //        Country = "Test Country 3",
            //        AccessViewStatus = "Test AccessViewStatus",
            //        QuoteSharingAllowed = "Test AccessViewStatus",
            //        TextToSpeechPermission = "Test TextToSpeechPermission",
            //        WebReaderLink = "Test WebReaderLink",
            //        Kind = "Test Kind",
            //        SelfLink = "Test SelfLink",
            //        Authors = new string[] { "Test Author" },
            //        CanonicalVolumeLink = "Test CanonicalVolumeLink",
            //        Description = "Test Description 3",
            //        Categories = new string[] { "Test Category" },
            //        InfoLink = "Test InfoLink",
            //        Language = "Test Languge",
            //        PageCount = 3
            //    },

            //    new BookDetailsForCatalog
            //    {
            //        Country = "Test Country 4",
            //        AccessViewStatus = "Test AccessViewStatus",
            //        QuoteSharingAllowed = "Test AccessViewStatus",
            //        TextToSpeechPermission = "Test TextToSpeechPermission",
            //        WebReaderLink = "Test WebReaderLink",
            //        Kind = "Test Kind",
            //        SelfLink = "Test SelfLink",
            //        Authors = new string[] { "Test Author" },
            //        CanonicalVolumeLink = "Test CanonicalVolumeLink",
            //        Description = "Test Description 4",
            //        Categories = new string[] { "Test Category" },
            //        InfoLink = "Test InfoLink",
            //        Language = "Test Languge",
            //        PageCount = 4
            //    },

            //    new BookDetailsForCatalog
            //    {
            //        Country = "Test Country 5",
            //        AccessViewStatus = "Test AccessViewStatus",
            //        QuoteSharingAllowed = "Test AccessViewStatus",
            //        TextToSpeechPermission = "Test TextToSpeechPermission",
            //        WebReaderLink = "Test WebReaderLink",
            //        Kind = "Test Kind",
            //        SelfLink = "Test SelfLink",
            //        Authors = new string[] { "Test Author" },
            //        CanonicalVolumeLink = "Test CanonicalVolumeLink",
            //        Description = "Test Description 5",
            //        Categories = new string[] { "Test Category" },
            //        InfoLink = "Test InfoLink",
            //        Language = "Test Languge",
            //        PageCount = 5
            //    }
            //};
            //_mockedMapperService.Setup(s => s.Map<List<BookDetailsForCatalog>>(googleClientResult.Content.Items)).Returns(mapperServiceResult);

            //var booksCatalogPaging = new PagingCatalogResult
            //(
            //    keywords,
            //    pageNumber,
            //    pageSize,
            //    5
            //);

            //var expectedResult = new Ok<GoogleBooksCatalog>(googleClientResult.Content);               

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            var booksCatalogParameter = new Domain.BooksCatalog(keywords, pageNumber, pageSize);

            // Act
            var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.Content).Equals(actualResult.Content);
            //Check.That(expectedResult.Content.PagingInfo.PageNumber).Equals(actualResult.Content.PagingInfo.PageNumber);
            //Check.That(expectedResult.Content.PagingInfo.PageSize).Equals(actualResult.Content.PagingInfo.PageSize);
            //Check.That(expectedResult.Content.PagingInfo.TotalItems).Equals(actualResult.Content.PagingInfo.TotalItems);

            //for (var i = 0; i < actualResult.Content.TotalItems; i++)
            //{
            //    Check.That(expectedResult.Content.BooksCatalog.BookDetails[i]).Equals(actualResult.Content.BooksCatalog.BookDetails[i]);
            //}
        }

        [Fact(DisplayName = "Should respond with an invalid parameter exception because of null 'booksCatalogSearch' argument")]
        public async void Should_RespondInvalidParameterException()
        {
            // Prepare
            Domain.BooksCatalog booksCatalogParameter = null;

            var expectedResult = new BadRequest("Bad request");

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(expectedResult.ErrorMessage).Equals(actualResult.ErrorMessage);
        }

        [Fact(DisplayName = "Should respond with an empty catalog because keywords were not found")]
        public async void Should_RespondEmptyCatalogWhenKeywordsNotFound()
        {
            // Prepare
            var kind = "Test Kind";

            var keywords = "Test Keywords";
            var pageSize = 100;
            var pageNumber = 0;

            var booksCatalogPaging = new PagingCatalogResult
            (
                keywords,
                pageNumber,
                pageSize,
                0
            );
            var expectedResult = new NoContent<BooksCatalogResult>
            (
                "No content was found",
                new BooksCatalogResult(
                    new BooksCatalogSearchResult
                    (
                        booksCatalogPaging,
                        new BooksCatalog(kind, new List<BookDetailsForCatalog>())
                    )
                )
            );

            var googleClientResult = new NoContent<GoogleBooksCatalog>(
                "No content was found",
                new GoogleBooksCatalog
                {
                    Kind = kind,
                    Items = null,
                    TotalItems = 0
                }
            );
            
            _mockedGoogleClientService.Setup(s => s.GetBooksCatalogAsync(keywords, pageSize, pageNumber)).ReturnsAsync(googleClientResult);

            var booksCatalogParameter = new Domain.BooksCatalog(keywords, pageNumber, pageSize);

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

            // Test
            //Check.That(expectedResult.Status).Equals(actualResult.Status);
            //Check.That(expectedResult.Content.BooksCatalog.BookDetails).Equals(actualResult.Content.BooksCatalog.BookDetails);
            //Check.That(expectedResult.Content.BooksCatalog.Kind).Equals(actualResult.Content.BooksCatalog.Kind);
            //Check.That(expectedResult.Content.PagingInfo.Keywords).Equals(actualResult.Content.PagingInfo.Keywords);
            //Check.That(expectedResult.Content.PagingInfo.PageNumber).Equals(actualResult.Content.PagingInfo.PageNumber);
            //Check.That(expectedResult.Content.PagingInfo.PageSize).Equals(actualResult.Content.PagingInfo.PageSize);
            //Check.That(expectedResult.Content.PagingInfo.TotalItems).Equals(actualResult.Content.PagingInfo.TotalItems);
        }

        [Fact(DisplayName = "Should respond with an internal server exception because the google client failed")]
        public async void Should_RespondInternalServerExceptionWhenFailingOnGoogleClient()
        {
            // Prepare
            var keywords = "Test Keywords";
            var pageSize = 100;
            var pageNumber = 0;

            var googleClientResult = new GoogleBooksCatalog();

            var expectedResult = new InternalServerError("Un error occured");
            var expectedException = 

            _mockedGoogleClientService
                .Setup(s => s.GetBooksCatalogAsync(keywords, pageSize, pageNumber))
                .Throws(new Exception("Google client unexpected exception"));

            var booksCatalogParameter = new Domain.BooksCatalog(keywords, pageNumber, pageSize);

            _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

            // Act
            var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

            // Test
            Check.That(expectedResult.Status).Equals(actualResult.Status);
            Check.That(actualResult.Exception).IsInstanceOf<Exception>();
        }

        //[Fact(DisplayName = "Should respond with an internal server exception because the mapper service failed")]
        //public async void Should_RespondInternalServerExceptionWhenFailingOnMapperService()
        //{
        //    // Prepare
        //    var keywords = "Test Keywords";
        //    var pageSize = 100;
        //    var pageNumber = 0;

        //    var googleClientResult = new InternalServerError<GoogleBooksCatalog>(
        //        "",
        //        new Exception(),
        //        new GoogleBooksCatalog
        //        {
        //            Kind = "Test Kind",
        //            TotalItems = 1,
        //            Items = new GoogleBookDetailsLite[]
        //            {
        //                new GoogleBookDetailsLite
        //                {
        //                    AccessInfo = new AccessInfo
        //                    {
        //                        Country = "Test Country 1",
        //                        AccessViewStatus = "Test AccessViewStatus",
        //                        QuoteSharingAllowed = "Test AccessViewStatus",
        //                        TextToSpeechPermission = "Test TextToSpeechPermission",
        //                        Viewability = "Test Viewability",
        //                        WebReaderLink = "Test WebReaderLink"
        //                    },
        //                    Kind = "Test Kind",
        //                    SelfLink = "Test SelfLink",
        //                    SaleInfo = new SaleInfoFull
        //                    {
        //                        ListPrice = new ListPrice
        //                        {
        //                            Amount = 25,
        //                            CurrencyCode = "EUR"
        //                        }
        //                    },
        //                    VolumeInfo = new VolumeInfoLite
        //                    {
        //                        Authors = new string[] { "Test Author" },
        //                        CanonicalVolumeLink = "Test CanonicalVolumeLink",
        //                        Description = "Test Description 1",
        //                        Categories = new string[] { "Test Category" },
        //                        InfoLink = "Test InfoLink",
        //                        Language = "Test Languge",
        //                        PageCount = 1,
        //                    },
        //                }
        //            }
        //        }
        //    );

        //    var expectedResult = new InternalServerError("Un error occured");

        //    _mockedGoogleClientService
        //        .Setup(s => s.GetBooksCatalogAsync(keywords, pageSize, pageNumber))
        //        .ReturnsAsync(googleClientResult);

        //    _mockedMapperService
        //        .Setup(s => s.Map<List<BookDetailsForCatalog>>(googleClientResult.Content.Items))
        //        .Throws(new Exception("Mapper service unexpected exception"));

        //    var booksCatalogParameter = new Domain.BooksCatalog(keywords, pageNumber, pageSize);
        //    _bookService = new BooksService(_mockedGoogleClientService.Object, _mockedMapperService.Object, _logger);

        //    // Act
        //    var actualResult = await _bookService.GetBooksCatalogAsync(booksCatalogParameter);

        //    // Test
        //    Check.That(expectedResult.Status).Equals(actualResult.Status);
        //    Check.That(actualResult.Exception).IsInstanceOf<Exception>();
        //}
    }
}
