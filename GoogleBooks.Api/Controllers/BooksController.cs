using GoogleBooks.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly IGoogleBooksClientService _googleBooksClient;

        public BooksController(IGoogleBooksClientService googleBooksClientService)
        {
            _googleBooksClient = googleBooksClientService;
        }

        [HttpGet]
        [Route("GetBooksByKeyword")]
        public async Task<IActionResult> GetBooksByKeyword(string keywords, int maxResults)
        {
            var defaultBooksResult = await _googleBooksClient.GetBooksByKeywordAsync(keywords, maxResults);

            return Ok(defaultBooksResult);
        }
    }
}
