using GoogleBooks.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        [Route("GetBooksByKeyword")]
        public async Task<IActionResult> GetBooksByKeyword(string keywords, int maxResults)
        {
            return Ok(await _booksService.GetBooksByKeywordAsync(keywords, maxResults));
        }
    }
}
