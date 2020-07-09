namespace GoogleBooks.Api.Helpers
{
    public static class ExceptionMessages
    {
        public const string InvalidKeyword = "You must at least enter a two character keyword";

        private const string NotFound = "The Id: \" id \" was not found";

        public const string InvalidPageNumber = "The page number cannot be lower than zero";

        public const string InvalidPageSize = "The page size cannot be lower than one";

        public const string EmptyId = "The book id cannot be empty";

        public const string InvalidIdLength = "The book id must be 12 characters long";

        public static string GetNotFoundMessage(string id)
        {
            return NotFound.Replace("id", id);
        }
    }
}
