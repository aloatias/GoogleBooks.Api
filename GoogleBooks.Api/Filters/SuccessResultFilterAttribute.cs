using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Filters
{
    public class SuccessResultFilterAttribute : ResultFilterAttribute
    {
        protected ObjectResult ActionResult { get; private set; }

        protected async Task CheckResultAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            ActionResult = context.Result as ObjectResult;
            if (ActionResult?.Value == null
                || ActionResult.StatusCode < 200
                || ActionResult.StatusCode >= 300)
            {
                await next();
                return;
            }
        }
    }
}