namespace GoogleBooks.Api.Dtos.Output.Exceptions
{
    public class InvalidBookException : ErrorBase
    {
        public InvalidBookException(string message) : base(message)
        {
        }
    }
}
