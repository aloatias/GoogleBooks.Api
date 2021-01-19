namespace GoogleBooks.Api.Helpers
{
    public static class ExceptionMessages
    {
        public const string InvalidKeyword = "You must at least enter a two character keyword";
        public const string NullArgument = "Object cannot be null";
        public const string InvalidPageNumber = "The page number cannot be lower than zero";
        public const string InvalidPageSize = "The page size cannot be lower than one";
        public const string EmptyId = "The book id cannot be empty";
        public const string InvalidIdLength = "The book id must be 12 characters long";
    }
}
