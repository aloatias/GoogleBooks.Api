using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Interfaces;
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
        private readonly IBooksService _booksService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBooksService booksService, ILogger<BooksController> logger)
        {
            _booksService = booksService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetBookDetailsById")]
        public async Task<IActionResult> GetBookDetailsAsync(string bookId)
        {
            try
            {
                return Ok(await _booksService.GetBookDetailsAsync(bookId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBookDetailsAsync) }");
                throw;
            }
        }

        [HttpGet]
        [Route("GetBooksCatalog")]
        public async Task<IActionResult> GetBooksCatalogAsync([FromBody]BooksCatalogSearch catalogBooksSearch)
        {
            try
            {
                return Ok(await _booksService.GetBooksCatalogAsync(catalogBooksSearch));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                throw;
            }
        }
    }
}
