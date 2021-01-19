using GoogleBooks.Api.Domain;
using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
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
        /// 
        /// <remarks>
        /// Sample request:
        ///
        ///     bookId = W7Y7CwAAQBAJ
        ///
        /// </remarks>
        /// 
        /// <param name="bookId"></param>
        /// <returns>A book's details</returns>
        /// <response code="200">Returns a book's details</response>
        /// <response code="204">If the book's Id wasn't found</response>
        /// <response code="400">If a Book's Id has an incorrect format</response>
        /// <response code="500">If there's a server error</response>
        [HttpGet]
        [Route("GetBookDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookDetailsAsync(string bookId)
        {
            try
            {
                // Create valid book
                var book = _domainFactory.CreateBook(bookId);

                var bookDetailsResult = await _booksService.GetBookDetailsAsync(book);

                switch (bookDetailsResult.Status)
                {
                    case HttpStatusCode.OK:
                        return Ok(bookDetailsResult.Content);
                    case HttpStatusCode.NotFound:
                        return StatusCode(204, bookDetailsResult.ErrorMessage);
                    case HttpStatusCode.BadRequest:
                        return BadRequest(bookDetailsResult.ErrorMessage);
                    default:
                        return StatusCode(500, bookDetailsResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBookDetailsAsync) }");
                return StatusCode(500, ((InvalidBookException)ex).Message);
            }
        }

        /// <summary>
        /// Gets a book catalog with the matching keyword parameter. The response will also be paged by the entered parameters
        /// </summary>
        ///<remarks>
        /// Sample request:
        ///
        ///     {
        ///        "keywords": ".net core development",
        ///        "pageSize": 10,
        ///        "pageNumber": 0
        ///     }
        ///
        /// </remarks>
        /// <param name="booksCatalogSearch"></param>
        /// <returns>A books catalog</returns>
        /// <response code="200">Returns a books catalog</response>
        /// <response code="400">If the "BooksCatalogSearch" parameter has a bad format</response>
        /// <response code="500">If there's a server error</response>
        [HttpPost]
        [Route("GetBooksCatalog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                    case HttpStatusCode.OK:
                        return Ok(booksCatalogResult);
                    case HttpStatusCode.BadRequest:
                        return BadRequest(booksCatalogResult.ErrorMessage);
                    default:
                        return StatusCode(500, booksCatalogResult.ErrorMessage);
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
