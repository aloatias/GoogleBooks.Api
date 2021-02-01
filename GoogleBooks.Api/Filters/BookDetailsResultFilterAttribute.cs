using AutoMapper;
using GoogleBooks.Api.Dtos.Output;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Filters
{
    public class BookDetailsResultFilterAttribute : SuccessResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await CheckResultAsync(context, next);

            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();

            ActionResult.Value = mapper.Map<IndividualBookDetails>(ActionResult.Value);

            await next();
        }
    }
}
