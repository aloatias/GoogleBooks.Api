using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly IDomainFactory _domainFactory;
        private readonly IBooksService _booksService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(
            IDomainFactory domainFactory,
            IBooksService booksService,
            ILogger<BooksController> logger)
        {
            _domainFactory = domainFactory;
            _booksService = booksService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetBookDetails")]
        public async Task<IActionResult> GetBookDetailsAsync(string bookId)
        {
            try
            {
                // Create valid book
                var book = _domainFactory.CreateBook(bookId);

                var bookDetailsResult = await _booksService.GetBookDetailsAsync(book);

                switch (bookDetailsResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok(bookDetailsResult.IndividualBookDetails);
                    case StatusEnum.NotFound:
                        return StatusCode(204, bookDetailsResult.Error.Message);
                    default:
                        return StatusCode(500, bookDetailsResult.Error.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBookDetailsAsync) }");
                return StatusCode(500, ((InvalidBookException)ex).Message);
            }
        }

        [HttpGet]
        [Route("GetBooksCatalogOnApiLaunch")]
        public async Task<IActionResult> GetBooksCatalogOnApiLaunchAsync(string keywords, int pageNumber, int pageSize)
        {
            try
            {
                // Create valid book catalog
                var booksCatalogSearch = _domainFactory.CreateBooksCatalog(
                     keywords,
                     pageNumber,
                     pageSize
                 );

                var booksCatalogResult = await _booksService.GetBooksCatalogAsync(booksCatalogSearch);

                if (booksCatalogResult.Status != StatusEnum.Ok)
                {
                    return StatusCode(500, booksCatalogResult.Error.Message);
                }

                return Ok(booksCatalogResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBooksCatalogOnApiLaunchAsync) }");
                return StatusCode(500, ((InvalidBooksCatalogException)ex).Message);
            }
        }

        [HttpPost]
        [Route("GetBooksCatalog")]
        public async Task<IActionResult> GetBooksCatalogAsync(BooksCatalogSearch catalogBooksSearch)
        {
            try
            {
                // Create valid book catalog
                var booksCatalogSearch = _domainFactory.CreateBooksCatalog(
                    catalogBooksSearch.Keywords,
                    catalogBooksSearch.PageNumber,
                    catalogBooksSearch.PageSize
                );

                var booksCatalogResult = await _booksService.GetBooksCatalogAsync(booksCatalogSearch);

                if (booksCatalogResult.Status != StatusEnum.Ok)
                {
                    return StatusCode(500, booksCatalogResult.Error.Message);
                }

                return Ok(booksCatalogResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                return StatusCode(500, ((InvalidBooksCatalogException)ex).Message);
            }
        }
    }
}
