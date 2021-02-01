using AutoMapper;
using GoogleBooks.Api.Dtos.Output;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Filters
{
    public class BookCatalogResultFilter : SuccessResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await CheckResultAsync(context, next);

            //var booksCatalogPaging = new PagingCatalogResult
            //(
            //    booksCatalogSearch.Keywords,
            //    booksCatalogSearch.PageNumber,
            //    booksCatalogSearch.PageSize,
            //    booksCatalogResult.Content.TotalItems
            //);

            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();
            //List<BookDetailsForCatalog> bookDetails = _mapper.Map<List<BookDetailsForCatalog>>(booksCatalogResult.Content.Items);

            //var booksCatalog = new DtosBooksCatalog(booksCatalogResult.Content.Kind, bookDetails);
            //var booksCatalogSearchResult = new BooksCatalogSearchResult(booksCatalogPaging, booksCatalog);

            ActionResult.Value = mapper.Map<IEnumerable<BookDetailsForCatalog>>(ActionResult.Value);

            await next();
        }
    }
}
