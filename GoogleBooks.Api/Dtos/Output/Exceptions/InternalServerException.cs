namespace GoogleBooks.Api.Dtos.Output.Exceptions
{
    public class InternalServerException : ErrorBase
    {
        public InternalServerException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
