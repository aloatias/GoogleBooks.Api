using GoogleBooks.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoogleBooks.Api.Controllers
{
    public class ControllerBase : Controller
    {
        protected IActionResult SendResponse<T>(IActionResponse<T> response) where T : class
        {
            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    return Ok(response);
                case HttpStatusCode.NotFound:
                    return NotFound(response);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response);
                case HttpStatusCode.NoContent:
                    return NoContent();
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
