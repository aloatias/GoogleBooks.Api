using AutoMapper;
using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Helpers;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainBooksCatalog = GoogleBooks.Api.Domain.BooksCatalog;
using DtosBooksCatalog = GoogleBooks.Api.Dtos.Output.BooksCatalog;

namespace GoogleBooks.Api.Services
{
    public class BooksService : IBooksService
    {
        private readonly IGoogleBooksClientService _googleBooksClientService;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksService> _logger;

        public BooksService
        (
            IGoogleBooksClientService googleBooksClientService,
            IMapper mapper,
            ILogger<BooksService> logger
        )
        {
            _googleBooksClientService = googleBooksClientService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IndividualBookDetailsResult> GetBookDetailsAsync(Book book)
        {
            try
            {
                if (book == null)
                {
                    return new IndividualBookDetailsResult(new InvalidBookException(ExceptionMessages.NullArgument), StatusEnum.InvalidParamater);
                }

                var individualBookDetails = await _googleBooksClientService.GetBookDetailsAsync(book.Id);
                if (individualBookDetails == null)
                {
                    return new IndividualBookDetailsResult
                    (
                        new NotFoundException(ExceptionMessages.GetNotFoundMessage(book.Id)), StatusEnum.NotFound
                    );
                }

                IndividualBookDetails bookDetails = _mapper.Map<IndividualBookDetails>(individualBookDetails);

                return new IndividualBookDetailsResult(bookDetails, StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBookDetailsAsync) }");
                return new IndividualBookDetailsResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }

        public async Task<BooksCatalogResult> GetBooksCatalogAsync(DomainBooksCatalog booksCatalogSearch)
        {
            try
            {
                if (booksCatalogSearch == null)
                {
                    return new BooksCatalogResult(new InvalidBookException(ExceptionMessages.NullArgument), StatusEnum.InvalidParamater);
                }

                var booksCatalogResult = await _googleBooksClientService.GetBooksCatalogAsync
                (
                    booksCatalogSearch.Keywords,
                    booksCatalogSearch.PageSize,
                    booksCatalogSearch.PageNumber
                );

                var booksCatalogPaging = new PagingCatalogResult
                (
                    booksCatalogSearch.Keywords,
                    booksCatalogSearch.PageNumber,
                    booksCatalogSearch.PageSize,
                    booksCatalogResult.TotalItems
                );

                if (booksCatalogResult.Items == null)
                {
                    return new BooksCatalogResult
                    (
                        new BooksCatalogSearchResult
                        (
                            booksCatalogPaging,
                            new DtosBooksCatalog
                            (
                                booksCatalogResult.Kind,
                                new List<BookDetailsForCatalog>()
                            )
                        ),
                        StatusEnum.Ok
                    );
                }

                List<BookDetailsForCatalog> bookDetails = _mapper.Map<List<BookDetailsForCatalog>>(booksCatalogResult.Items);

                var booksCatalog = new DtosBooksCatalog(booksCatalogResult.Kind, bookDetails);
                var booksCatalogSearchResult = new BooksCatalogSearchResult(booksCatalogPaging, booksCatalog);

                return new BooksCatalogResult(booksCatalogSearchResult, StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                return new BooksCatalogResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }

        #region Private Methods
        private List<BookDetailsForCatalog> MapBookCatalogDataToResultDto(GoogleBooksCatalog booksCatalogResult)
        {
            var bookDetailsForCatalog = new List<BookDetailsForCatalog>();
            foreach (var book in booksCatalogResult.Items)
            {
                bookDetailsForCatalog.Add
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
                        //ImageReadingMode = book.VolumeInfo?.ReadingModes?.Image,
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
                        QuoteSharingAllowed = book.AccessInfo?.QuoteSharingAllowed,
                        Categories = book.VolumeInfo?.Categories ?? null
                    }
                );
            }

            return bookDetailsForCatalog;
        }

        #endregion
    }
}
