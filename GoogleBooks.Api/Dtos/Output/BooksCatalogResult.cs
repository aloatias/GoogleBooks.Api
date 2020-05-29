﻿using GoogleBooks.Api.Dtos.Output.Exceptions;

namespace GoogleBooks.Api.Dtos.Output
{
    public class BooksCatalogResult : ResultBase
    {
        public PagingInfoResult PagingInfo { get; private set; }

        public BooksCatalog BooksCatalog { get; private set; }

        public BooksCatalogResult(BooksCatalogSearchResult booksCatalogSearchResult, StatusEnum status) : base(status)
        {
            BooksCatalog = booksCatalogSearchResult.BooksCatalog;
            PagingInfo = booksCatalogSearchResult.PagingInfoResult;
        }

        public BooksCatalogResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}
