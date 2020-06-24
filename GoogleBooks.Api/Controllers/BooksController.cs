using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Dtos.Output;
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
        private readonly IBooksService _booksService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBooksService booksService, ILogger<BooksController> logger)
        {
            _booksService = booksService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetBookDetailsById")]
        public async Task<IActionResult> GetBookDetailsByIdAsync(string bookId)
        {
            try
            {
                var bookDetailsResult = await _booksService.GetBookDetailsByIdAsync(bookId);

                switch (bookDetailsResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok(bookDetailsResult.IndividualBookDetails);
                    case StatusEnum.NotFound:
                        return StatusCode(204, bookDetailsResult.Error.ErrorMessage);
                    default:
                        return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBookDetailsByIdAsync) }");
                throw;
            }
        }

        [HttpGet]
        [Route("GetBooksCatalogOnApiLaunch")]
        public async Task<IActionResult> GetBooksCatalogOnApiLaunchAsync(string keywords, int pageNumber, int pageSize)
        {
            try
            {
                var bookCatalogResult = await _booksService.GetBooksCatalogAsync(new BooksCatalogSearch { Keywords = keywords, PageNumber = pageNumber, PageSize = pageSize });

                switch (bookCatalogResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok(bookCatalogResult);
                    case StatusEnum.NotFound:
                        return NotFound(bookCatalogResult.Error.ErrorMessage);
                    default:
                        return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBooksCatalogOnApiLaunchAsync) }");
                throw;
            }
        }

        [HttpPost]
        [Route("GetBooksCatalog")]
        public async Task<IActionResult> GetBooksCatalogAsync(BooksCatalogSearch catalogBooksSearch)
        {
            try
            {
                var getBooksCatalogResult = await _booksService.GetBooksCatalogAsync(catalogBooksSearch);

                switch (getBooksCatalogResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok(getBooksCatalogResult);
                    case StatusEnum.InvalidParamater:
                        return BadRequest(getBooksCatalogResult.Error.ErrorMessage);
                    default:
                        return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                throw;
            }
        }
    }
}
