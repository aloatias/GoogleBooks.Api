using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Filters
{
    public class SuccessResultFilterAttribute : ResultFilterAttribute
    {
        protected ObjectResult ResultFromAction { get; private set; }

        protected async Task CheckResultAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            ResultFromAction = context.Result as ObjectResult;
            if (ResultFromAction?.Value == null
                || ResultFromAction.StatusCode < 200
                || ResultFromAction.StatusCode >= 300)
            {
                await next();
                return;
            }
        }
    }
}