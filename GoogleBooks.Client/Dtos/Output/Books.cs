namespace GoogleBooks.Client.Dtos.Output
{
    public class Books
    {
        public string Kind { get; set; }

        public int TotalItems { get; set; }

        public BookDetailsLite[] Items { get; set; }
    }
}