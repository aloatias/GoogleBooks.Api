using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Helpers;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Services
{
    public class BooksService : IBooksService
    {
        private readonly IGoogleBooksClientService _googleBooksClientService;
        private readonly ILogger<BooksService> _logger;

        public BooksService(IGoogleBooksClientService googleBooksClientService, ILogger<BooksService> logger)
        {
            _googleBooksClientService = googleBooksClientService;
            _logger = logger;
        }

        public async Task<IndividualBookDetailsResult> GetBookDetailsAsync(string bookId)
        {
            try
            {
                var individualBookDetails = await _googleBooksClientService.GetBookDetailsAsync(bookId);
                if (individualBookDetails == null)
                {
                    return new IndividualBookDetailsResult(new NotFoundException(ExceptionMessages.GetNotFoundMessage(bookId)), StatusEnum.NotFound);
                }

                var bookDetails = new IndividualBookDetails
                {
                    Id = individualBookDetails.Id,
                    Title = individualBookDetails.VolumeInfo.Title,
                    Description = individualBookDetails.VolumeInfo.Description,
                    Etag = individualBookDetails.Etag,
                    Authors = individualBookDetails.VolumeInfo.Authors,
                    Publisher = individualBookDetails.VolumeInfo.Publisher,
                    PublishedDate = individualBookDetails.VolumeInfo.PublishedDate,
                    SmallImage = individualBookDetails.VolumeInfo?.ImageLinks?.Small,
                    MediumImage = individualBookDetails.VolumeInfo?.ImageLinks?.Medium,
                    LargeImage = individualBookDetails.VolumeInfo?.ImageLinks?.Large,
                    ExtraLargeImage = individualBookDetails.VolumeInfo?.ImageLinks?.ExtraLarge,
                    SmallThumbnail = individualBookDetails.VolumeInfo?.ImageLinks?.SmallThumbnail,
                    Thumbnail = individualBookDetails.VolumeInfo?.ImageLinks?.Thumbnail,
                    Country = individualBookDetails.SaleInfo?.Country,
                    Saleability = individualBookDetails.SaleInfo?.Saleability,
                    IsEbook = individualBookDetails.SaleInfo?.IsEbook,
                    Viewability = individualBookDetails.AccessInfo?.Viewability,
                    Embeddable = individualBookDetails.AccessInfo?.Embeddable,
                    PublicDomain = individualBookDetails.AccessInfo?.PublicDomain,
                    IsPdfAvailable = individualBookDetails.AccessInfo?.Pdf?.IsAvailable,
                    PdfActsTokenLink = individualBookDetails.AccessInfo?.Pdf?.ActsTokenLink,
                    WebReaderLink = individualBookDetails.AccessInfo?.WebReaderLink,
                    AccessViewStatus = individualBookDetails.AccessInfo?.AccessViewStatus,
                    QuoteSharingAllowed = individualBookDetails.AccessInfo?.QuoteSharingAllowed
                };

                return new IndividualBookDetailsResult(bookDetails, StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBookDetailsAsync) }");
                return new IndividualBookDetailsResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }

        public async Task<BooksCatalogResult> GetBooksCatalogAsync(BooksCatalogSearch booksCatalogSearch)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(booksCatalogSearch.Keywords)
                    || booksCatalogSearch.Keywords.Length < 2)
                {
                    return new BooksCatalogResult(new InvalidKeywordException(ExceptionMessages.InvalidKeyword), StatusEnum.InvalidParamater);
                }

                var booksCatalogResult = await _googleBooksClientService.GetBooksCatalogAsync(
                    booksCatalogSearch.Keywords,
                    booksCatalogSearch.PageSize,
                    booksCatalogSearch.PageNumber
                );

                var pagingInfoResult = new PagingInfoResult(booksCatalogSearch.Keywords, booksCatalogSearch.PageNumber, booksCatalogSearch.PageSize, booksCatalogResult.TotalItems);

                if (booksCatalogResult.Items == null)
                {
                    return new BooksCatalogResult(
                        new BooksCatalogSearchResult(
                            pagingInfoResult,
                            new BooksCatalog(
                                booksCatalogResult.Kind,
                                new List<BookDetailsForCatalog>())),
                                StatusEnum.Ok
                            );
                }

                var bookDetails = new List<BookDetailsForCatalog>();
                foreach (var book in booksCatalogResult.Items)
                {
                    bookDetails.Add
                    (
                        new BookDetailsForCatalog
                        {
                            Id = book.Id,
                            Kind = book.Kind,
                            Etag = book.Etag,
                            SelfLink = book.SelfLink,
                            Title = book.VolumeInfo?.Title,
                            Authors = book.VolumeInfo?.Authors,
                            Publisher = book.VolumeInfo?.Publisher,
                            PublishedDate = book.VolumeInfo?.PublishedDate,
                            Description = book.VolumeInfo?.Description,
                            TextReadingMode = book.VolumeInfo?.ReadingModes.Text,
                            ImageReadingMode = book.VolumeInfo?.ReadingModes?.Image,
                            PageCount = book.VolumeInfo?.PageCount,
                            PrintedPageCount = book.VolumeInfo?.PrintedPageCount,
                            PrintType = book.VolumeInfo?.PrintType,
                            MaturityRating = book.VolumeInfo?.MaturityRating,
                            AllowAnonLogging = book.VolumeInfo?.AllowAnonLogging,
                            ContentVersion = book.VolumeInfo?.ContentVersion,
                            Language = book.VolumeInfo?.Language,
                            PreviewLink = book.VolumeInfo?.PreviewLink,
                            InfoLink = book.VolumeInfo?.InfoLink,
                            CanonicalVolumeLink = book.VolumeInfo?.CanonicalVolumeLink,
                            SmallThumbNail = book.VolumeInfo.ImageLinks?.SmallThumbnail,
                            Thumbnail = book.VolumeInfo.ImageLinks?.Thumbnail,
                            Country = book.AccessInfo?.Country,
                            Saleability = book.SaleInfo?.Saleability,
                            IsEbook = book.SaleInfo?.IsEbook,
                            Embeddable = book.AccessInfo?.Embeddable,
                            PublicDomain = book.AccessInfo?.PublicDomain,
                            TextToSpeechPermission = book.AccessInfo?.TextToSpeechPermission,
                            IsPdfAvailable = book.AccessInfo?.Pdf?.IsAvailable,
                            PdfActsTokenLink = book.AccessInfo?.Pdf?.ActsTokenLink,
                            WebReaderLink = book.AccessInfo?.WebReaderLink,
                            AccessViewStatus = book.AccessInfo?.AccessViewStatus,
                            QuoteSharingAllowed = book.AccessInfo?.QuoteSharingAllowed
                        }
                    );
                }

                var booksCatalog = new BooksCatalog(booksCatalogResult.Kind, bookDetails);
                var booksCatalogSearchResult = new BooksCatalogSearchResult(pagingInfoResult, booksCatalog);

                return new BooksCatalogResult(booksCatalogSearchResult, StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                return new BooksCatalogResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }
    }
}
