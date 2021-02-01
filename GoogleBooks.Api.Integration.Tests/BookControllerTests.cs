using AutoMapper;
using GoogleBooks.Api.Controllers;
using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Infrastructure.Dtos;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GoogleBooks.Api.Integration.Tests
{
    public class BookControllerTests : TestBase
    {
        private readonly Mock<IBooksService> _mockedBookService;
        private readonly Mock<IGoogleBooksClientService> _mockedGoogleClientService;
        private readonly ILogger<BookController> _logger;
        private readonly Mock<IDomainFactory> _mockedDomainFactory;
        private BookController _bookController;

        public BookControllerTests()
        {
            _mockedGoogleClientService = MockService<IGoogleBooksClientService>();
            _logger = CreateLogger<BookController>();
            _mockedBookService = MockService<IBooksService>();
            _mockedDomainFactory = MockService<IDomainFactory>();
        }

        [Fact]
        public async Task TestBookCatalogResultFilter()
        {
            // Prepare
            var expectedServiceResult = new Ok<GoogleBooksCatalog>(new GoogleBooksCatalog
            {
                Kind = "Test",
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
                }
            });

            var bookCatalog = new BooksCatalog("harry potter", 0, 2);
            _mockedDomainFactory.Setup(x => x.CreateBooksCatalog(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(bookCatalog);
            
            _mockedBookService.Setup(x => x.GetBooksCatalogAsync(It.IsAny<BooksCatalog>())).ReturnsAsync(() => expectedServiceResult);
            
            _bookController = new BookController(_mockedDomainFactory.Object, _mockedBookService.Object, _logger);

            // Act
            await _bookController.GetBooksCatalogAsync(new BooksCatalogSearch { Keywords = "harry potter", PageNumber = 0, PageSize = 2 });
        }
    }
}
