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

        public BooksController
        (
            IDomainFactory domainFactory,
            IBooksService booksService,
            ILogger<BooksController> logger
        )
        {
            _domainFactory = domainFactory;
            _booksService = booksService;
            _logger = logger;
        }

        /// <summary>
        /// Gets a book's details by its Id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
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
                    case StatusEnum.InvalidParamater:
                        return BadRequest(bookDetailsResult.Error.Message);
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

        /// <summary>
        /// Gets a book catalog with matching the keyword parameter. The response will also be paged by the entered parameters
        /// </summary>
        /// <param name="booksCatalogSearch"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetBooksCatalog")]
        public async Task<IActionResult> GetBooksCatalogAsync(BooksCatalogSearch booksCatalogSearch)
        {
            try
            {
                // Create valid book catalog
                var checkedBooksCatalogSearch = _domainFactory.CreateBooksCatalog
                (
                    booksCatalogSearch.Keywords,
                    booksCatalogSearch.PageNumber,
                    booksCatalogSearch.PageSize
                );

                var booksCatalogResult = await _booksService.GetBooksCatalogAsync(checkedBooksCatalogSearch);

                switch (booksCatalogResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok(booksCatalogResult);
                    case StatusEnum.InvalidParamater:
                        return BadRequest(booksCatalogResult.Error.Message);
                    default:
                        return StatusCode(500, booksCatalogResult.Error.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                return StatusCode(500, ((InvalidBooksCatalogException)ex).Message);
            }
        }
    }
}
