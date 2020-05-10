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
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            await _googleBooksClientService.TestClient();

            return Ok("working");
        }
    }
}
