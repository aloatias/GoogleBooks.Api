using AutoMapper;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Client.Dtos.Output;
using GoogleBooks.Infrastructure.Interfaces;
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
            ActionResult.Value = mapper.Map<IndividualBookDetails>(((IActionResponse<GoogleBookDetailsFull>)(ActionResult.Value)).Content);

            await next();
        }
    }
}
