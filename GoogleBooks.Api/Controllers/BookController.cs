using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok("working");
        }
    }
}
