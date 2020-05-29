namespace GoogleBooks.Client.Configuration.ConfigurationOptions
{
    public class GoogleBooksUrlOptions
    {
        public string GetBookDetails { get; set; }

        public string GetBooksCatalog { get; set; }

        public string MaxResultsParameter { get; set; }

        public string StartIndexParameter { get; set; }
    }
}