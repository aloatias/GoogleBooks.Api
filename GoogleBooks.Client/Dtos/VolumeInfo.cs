namespace GoogleBooks.Client.Dtos
{
    public class VolumeInfo
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string[] Authors { get; set; }

        public string Publisher { get; set; }

        public string PublishedDate { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public string PreviewLink { get; set; }

        public string InfoLink { get; set; }

        public ImageLinks ImageLinks { get; set; }
    }
}