namespace GoogleBooks.Api.Dtos.Output.Exceptions
{
    public class NotFoundException : ErrorBase
    {
        public NotFoundException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
