namespace GoogleBooks.Api.Dtos.Output.Exceptions
{
    public class InvalidBookException : ErrorBase
    {
        public InvalidBookException(string error) : base(error)
        {
        }
    }
}
