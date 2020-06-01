namespace GoogleBooks.Api.Helpers
{
    public static class ExceptionMessages
    {
        public const string InvalidKeyword = "You must at least enter a two character keyword";

        private const string NotFound = "The Id: \" id \" was not found";

        public static string GetNotFoundMessage(string id)
        {
            return NotFound.Replace("id", id);
        }
    }
}
