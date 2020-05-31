namespace GoogleBooks.Api.Dtos.Output.Exceptions
{
    public class InvalidKeywordException : ErrorBase
    {
        public InvalidKeywordException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
