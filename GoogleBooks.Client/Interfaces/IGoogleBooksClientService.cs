﻿using GoogleBooks.Client.Dtos.Output;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Interfaces
{
    public interface IGoogleBooksClientService
    {
        Task<GoogleBookDetailsFull> GetBookDetailsByIdAsync(string bookId);

        Task<GoogleBooksCatalog> GetBooksCatalogAsync(string keywords, int pageSize, int pageNumber);
    }
}
