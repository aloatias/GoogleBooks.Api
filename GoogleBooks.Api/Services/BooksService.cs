using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public async Task<BookDetailsFullResult> GetBookDetailsAsync(string bookId)
        {
            try
            {
                var getBookDetailsResult = await _googleBooksClientService.GetBookDetailsAsync(bookId);
                if (getBookDetailsResult == null)
                {
                    return new BookDetailsFullResult(new NotFoundException($"The bookId: \"{ bookId }\" was not found"), StatusEnum.NotFound);
                }

                var bookDetails = new IndividualBookDetails
                {
                    Id = getBookDetailsResult.Id,
                    Etag = getBookDetailsResult.Etag,
                    SmallImage = getBookDetailsResult.VolumeInfo?.ImageLinks?.Small,
                    MediumImage = getBookDetailsResult.VolumeInfo?.ImageLinks?.Medium,
                    LargeImage = getBookDetailsResult.VolumeInfo?.ImageLinks?.Large,
                    ExtraLargeImage = getBookDetailsResult.VolumeInfo?.ImageLinks?.ExtraLarge,
                    SmallThumbnail = getBookDetailsResult.VolumeInfo?.ImageLinks?.SmallThumbnail,
                    Thumbnail = getBookDetailsResult.VolumeInfo?.ImageLinks?.Thumbnail,
                    Country = getBookDetailsResult.SaleInfo?.Country,
                    Saleability = getBookDetailsResult.SaleInfo?.Saleability,
                    IsEbook = getBookDetailsResult.SaleInfo?.IsEbook,
                    Viewability = getBookDetailsResult.AccessInfo?.Viewability,
                    Embeddable = getBookDetailsResult.AccessInfo?.Embeddable,
                    PublicDomain = getBookDetailsResult.AccessInfo?.PublicDomain,
                    IsPdfAvailable = getBookDetailsResult.AccessInfo?.Pdf?.IsAvailable,
                    PdfActsTokenLink = getBookDetailsResult.AccessInfo?.Pdf?.ActsTokenLink,
                    WebReaderLink = getBookDetailsResult.AccessInfo?.WebReaderLink,
                    AccessViewStatus = getBookDetailsResult.AccessInfo?.AccessViewStatus,
                    QuoteSharingAllowed = getBookDetailsResult.AccessInfo?.QuoteSharingAllowed
                };

                return new BookDetailsFullResult(bookDetails, StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBookDetailsAsync) }");
                return new BookDetailsFullResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }

        public async Task<BooksCatalogResult> GetBooksCatalogAsync(BooksCatalogSearch booksCatalogSearch)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(booksCatalogSearch.Keywords))
                {
                    return new BooksCatalogResult(new InvalidKeywordException("You must at least enter a two character keyword"), StatusEnum.InvalidParamater);
                }

                var getBooksCatalogResult = await _googleBooksClientService.GetBooksCatalogAsync(
                    booksCatalogSearch.Keywords,
                    booksCatalogSearch.PageSize,
                    booksCatalogSearch.PageNumber
                );

                var pagingInfoResult = new PagingInfoResult(booksCatalogSearch.Keywords, booksCatalogSearch.PageNumber, booksCatalogSearch.PageSize, getBooksCatalogResult.TotalItems);

                var bookDetails = new List<BookDetailsForCatalog>();
                foreach (var book in getBooksCatalogResult.Items)
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

                var booksCatalog = new BooksCatalog(getBooksCatalogResult.Kind, bookDetails);
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
