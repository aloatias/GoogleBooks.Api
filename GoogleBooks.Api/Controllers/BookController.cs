using GoogleBooks.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IGoogleBooksClientService _googleBooksClientService;

        public BookController(IGoogleBooksClientService googleBooksClientService)
        {
            _googleBooksClientService = googleBooksClientService;
        }

        [HttpGet]
        [Route("GetBooksByKeyword")]
        public async Task<IActionResult> GetBooksByKeyword(string keywords)
        {
            var defaultBooksResult = await _googleBooksClientService.GetBooksByKeyword(keywords);

            return Ok(defaultBooksResult);
        }

        [HttpGet]
        [Route("GetAllBooksDefault")]
        public async Task<IActionResult> GetAllBooksDefault()
        {
            var defaultBooksResult = await _googleBooksClientService.GetBooksByKeyword("tennis");

            return Ok(defaultBooksResult);
        }
    }
}
