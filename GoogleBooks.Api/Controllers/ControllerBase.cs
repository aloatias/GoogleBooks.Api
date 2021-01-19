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
                    return Ok(response.Content);
                case HttpStatusCode.NotFound:
                    return StatusCode(204, response.ErrorMessage);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.ErrorMessage);
                default:
                    return StatusCode(500, response.ErrorMessage);
            }
        }
    }
}
