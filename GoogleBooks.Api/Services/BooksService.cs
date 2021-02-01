using AutoMapper;
using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Interfaces;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Client.Interfaces;
using GoogleBooks.Infrastructure.Dtos;
using GoogleBooks.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using DomainBooksCatalog = GoogleBooks.Api.Domain.BooksCatalog;

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

        public async Task<IActionResponse<GoogleBookDetailsFull>> GetBookDetailsAsync(Book book)
        {
            try
            {
                if (book == null)
                {
                    return new BadRequest<GoogleBookDetailsFull>("Bad request");
                }

                var individualBookDetails = await _googleBooksClientService.GetBookDetailsAsync(book.Id);
                if (individualBookDetails?.Content == null)
                {
                    return new NotFound<GoogleBookDetailsFull>("The book Id doesn't exist");
                }

                return new Ok<GoogleBookDetailsFull>(individualBookDetails.Content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBookDetailsAsync) }");
                throw new InternalServerError<GoogleBookDetailsFull>(ex.Message, ex);
            }
        }

        public async Task<IActionResponse<GoogleBooksCatalog>> GetBooksCatalogAsync(DomainBooksCatalog booksCatalogSearch)
        {
            try
            {
                if (booksCatalogSearch == null)
                {
                    return new BadRequest<GoogleBooksCatalog>("Bad request");
                }

                var booksCatalogResult = await _googleBooksClientService.GetBooksCatalogAsync
                (
                    booksCatalogSearch.Keywords,
                    booksCatalogSearch.PageSize,
                    booksCatalogSearch.PageNumber
                );

                if (booksCatalogResult?.Content?.Items == null)
                {
                    return new NoContent<GoogleBooksCatalog>("No content was found");
                }

                return new Ok<GoogleBooksCatalog>(booksCatalogResult.Content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksService) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                throw new InternalServerError<GoogleBooksCatalog>(ex.Message, ex);
            }
        }
    }
}
