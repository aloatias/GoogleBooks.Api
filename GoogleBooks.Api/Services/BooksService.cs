using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Logging;
using System;
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

                var bookDetails = new BookDetailsFull
                {
                    Id = getBookDetailsResult.Id,
                    Etag = getBookDetailsResult.Etag,
                    SmallImage = getBookDetailsResult.VolumeInfo.ImageLinks.Small,
                    MediumImage = getBookDetailsResult.VolumeInfo.ImageLinks.Medium,
                    LargeImage = getBookDetailsResult.VolumeInfo.ImageLinks.Large,
                    ExtraLargeImage = getBookDetailsResult.VolumeInfo.ImageLinks.ExtraLarge,
                    SmallThumbnail = getBookDetailsResult.VolumeInfo.ImageLinks.SmallThumbnail,
                    Thumbnail = getBookDetailsResult.VolumeInfo.ImageLinks.Thumbnail,
                    Country = getBookDetailsResult.SaleInfo.Country,
                    Saleability = getBookDetailsResult.SaleInfo.Saleability,
                    IsEbook = getBookDetailsResult.SaleInfo.IsEbook,
                    Viewability = getBookDetailsResult.AccessInfo.Viewability,
                    Embeddable = getBookDetailsResult.AccessInfo.Embeddable,
                    PublicDomain = getBookDetailsResult.AccessInfo.PublicDomain,
                    IsPdfAvailable = getBookDetailsResult.AccessInfo.Pdf.IsAvailable,
                    PdfActsTokenLink = getBookDetailsResult.AccessInfo.Pdf.ActsTokenLink,
                    WebReaderLink = getBookDetailsResult.AccessInfo.WebReaderLink,
                    AccessViewStatus = getBookDetailsResult.AccessInfo.AccessViewStatus,
                    QuoteSharingAllowed = getBookDetailsResult.AccessInfo.QuoteSharingAllowed
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
                // TODO: Validate keywords before calling google client

                var getBooksCatalogResult = await _googleBooksClientService.GetBooksCatalogAsync(
                    booksCatalogSearch.Keywords,
                    booksCatalogSearch.PageSize,
                    booksCatalogSearch.PageNumber
                );

                // Map result to BooksCatalog

                var booksCatalogSearchResult = new BooksCatalogSearchResult
                {
                    PagingInfoResult = new PagingInfoResult
                    {
                        Keywords = booksCatalogSearch.Keywords,
                        PageNumber = booksCatalogSearch.PageNumber,
                        PageSize = booksCatalogSearch.PageSize,
                        TotalItems = getBooksCatalogResult.TotalItems
                    },

                    // BooksCatalog = // Map with result
                };

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
