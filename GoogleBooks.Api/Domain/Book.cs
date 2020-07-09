using GoogleBooks.Api.Dtos.Output.Exceptions;
using GoogleBooks.Api.Helpers;

namespace GoogleBooks.Api.Domain
{
    public class Book
    {
        public string Id { get; private set; }

        public Book(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
            {
                throw new InvalidBookException(ExceptionMessages.EmptyId);
            }

            if (bookId.Length != 12)
            {
                throw new InvalidBookException(ExceptionMessages.InvalidIdLength);
            }

            Id = bookId;
        }
    }
}
