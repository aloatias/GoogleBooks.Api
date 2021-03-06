﻿using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Helpers;

namespace GoogleBooks.Api.Domain
{
    public class Book
    {
        private const int _bookIdLength = 12;

        public string Id { get; private set; }

        public Book(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
            {
                throw new InvalidBookException(ExceptionMessages.EmptyId);
            }

            if (bookId.Length != _bookIdLength)
            {
                throw new InvalidBookException(ExceptionMessages.InvalidIdLength);
            }

            Id = bookId;
        }
    }
}
