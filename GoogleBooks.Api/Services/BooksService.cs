﻿using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Logging;
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

        public async Task<BookDetailsFullResult> GetBookDetailsByIdAsync(string bookId)
        {
            var result = await _googleBooksClientService.GetBookDetailsByIdAsync(bookId);

            var bookDetails = new BookDetailsFull
            {
                Id = result.Id,
                Etag = result.Etag,
                SmallImage = result.VolumeInfo.ImageLinks.Small,
                MediumImage = result.VolumeInfo.ImageLinks.Medium,
                LargeImage = result.VolumeInfo.ImageLinks.Large,
                ExtraLargeImage = result.VolumeInfo.ImageLinks.ExtraLarge,
            };

            return new BookDetailsFullResult(bookDetails, StatusEnum.Ok);
        }

        public async Task<BooksByKeywordResult> GetBooksByKeywordAsync(BooksCatalogSearch catalogBooksSearch)
        {
            // TODO: Validate keywords before calling google client

            var result = await _googleBooksClientService.GetBooksByKeywordAsync(catalogBooksSearch.Keywords, catalogBooksSearch.PageSize);

            // Map result to BooksCatalog

            return new BooksByKeywordResult(new BooksCatalog(), StatusEnum.Ok);
        }
    }
}
