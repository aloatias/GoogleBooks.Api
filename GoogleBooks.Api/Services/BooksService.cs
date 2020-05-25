using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
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
                var result = await _googleBooksClientService.GetBookDetailsAsync(bookId);

                var bookDetails = new BookDetailsFull
                {
                    Id = result.Id,
                    Etag = result.Etag,
                    SmallImage = result.VolumeInfo.ImageLinks.Small,
                    MediumImage = result.VolumeInfo.ImageLinks.Medium,
                    LargeImage = result.VolumeInfo.ImageLinks.Large,
                    ExtraLargeImage = result.VolumeInfo.ImageLinks.ExtraLarge,
                    // TODO: Match missing fields!!!
                };

                return new BookDetailsFullResult(bookDetails, StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBookDetailsAsync) }");
                throw;
            }
        }

        public async Task<BooksCatalogResult> GetBooksCatalogAsync(BooksCatalogSearch booksCatalogSearch)
        {
            try
            {
                // TODO: Validate keywords before calling google client

                var result = await _googleBooksClientService.GetBooksCatalogAsync(
                    booksCatalogSearch.Keywords,
                    booksCatalogSearch.PageSize,
                    booksCatalogSearch.PageNumber
                );

                // Map result to BooksCatalog

                var booksCatalogSearchResult = new BooksCatalogSearchResult
                {
                    Keywords = booksCatalogSearch.Keywords,
                    PageNumber = booksCatalogSearch.PageNumber,
                    PageSize = booksCatalogSearch.PageSize,
                    TotalItems = result.TotalItems
                };

                return new BooksCatalogResult(new BooksCatalog(), booksCatalogSearchResult, StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                throw;
            }
        }
    }
}
