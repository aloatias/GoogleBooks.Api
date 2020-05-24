using GoogleBooks.Api.Dtos;
using GoogleBooks.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            return Ok(await _booksService.GetBookDetailsByIdAsync(bookId));
        }

        [HttpGet]
        [Route("GetBooksByKeyword")]
        public async Task<IActionResult> GetBooksByKeyword(BooksCatalogSearch catalogBooksSearch)
        {
            return Ok(await _booksService.GetBooksByKeywordAsync(catalogBooksSearch));
        }
    }
}
