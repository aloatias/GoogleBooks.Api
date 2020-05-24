namespace GoogleBooks.Client.Dtos.Output
{
    public class GoogleBooksCatalog
    {
        public string Kind { get; set; }

        public int TotalItems { get; set; }

        public GoogleBookDetailsLite[] Items { get; set; }
    }
}