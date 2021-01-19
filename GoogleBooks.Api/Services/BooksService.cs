using AutoMapper;
using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Infrastructure.Dtos;
using GoogleBooks.Infrastructure.Interfaces;
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

        public async Task<IActionResponse<IndividualBookDetails>> GetBookDetailsAsync(Book book)
        {
            try
            {
                if (book == null)
                {
                    return new BadRequest<IndividualBookDetails>("Bad request");
                }

                var individualBookDetails = await _googleBooksClientService.GetBookDetailsAsync(book.Id);
                if (individualBookDetails == null)
                {
                    return new NotFound<IndividualBookDetails>("The book Id doesn't exist");
                }

                return new Ok<IndividualBookDetails>(_mapper.Map<IndividualBookDetails>(individualBookDetails));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBookDetailsAsync) }");
                return new InternalServerError<IndividualBookDetails>("An error occured", ex);
            }
        }

        public async Task<IActionResponse<BooksCatalogResult>> GetBooksCatalogAsync(DomainBooksCatalog booksCatalogSearch)
        {
            try
            {
                if (booksCatalogSearch == null)
                {
                    return new BadRequest<BooksCatalogResult>("Bad request");
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
                    booksCatalogResult.Content.TotalItems
                );

                if (booksCatalogResult.Content.Items == null)
                {
                    var noContentResponse =  new BooksCatalogResult
                    (
                        new BooksCatalogSearchResult
                        (
                            booksCatalogPaging,
                            new DtosBooksCatalog
                            (
                                booksCatalogResult.Content.Kind,
                                new List<BookDetailsForCatalog>()
                            )
                        )
                    );

                    return new NoContent<BooksCatalogResult>(noContentResponse, "No content was found");
                }

                List<BookDetailsForCatalog> bookDetails = _mapper.Map<List<BookDetailsForCatalog>>(booksCatalogResult.Content.Items);

                var booksCatalog = new DtosBooksCatalog(booksCatalogResult.Content.Kind, bookDetails);
                var booksCatalogSearchResult = new BooksCatalogSearchResult(booksCatalogPaging, booksCatalog);

                return new Ok<BooksCatalogResult>(new BooksCatalogResult(booksCatalogSearchResult));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                return new InternalServerError<BooksCatalogResult>(ex.Message, ex);
            }
        }
    }
}
