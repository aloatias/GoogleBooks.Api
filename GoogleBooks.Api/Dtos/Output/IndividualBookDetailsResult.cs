namespace GoogleBooks.Api.Dtos.Output
{
    public class IndividualBookDetailsResult
    {
        public IndividualBookDetails IndividualBookDetails { get; private set; }

        public IndividualBookDetailsResult(IndividualBookDetails individualBookDetails)
        {
            IndividualBookDetails = individualBookDetails;
        }
    }
}
