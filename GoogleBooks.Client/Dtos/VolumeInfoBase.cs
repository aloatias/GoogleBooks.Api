namespace GoogleBooks.Client.Dtos
{
    public class VolumeInfoBase
    {
        public string Title { get; set; }

        public string[] Authors { get; set; }

        public string Publisher { get; set; }

        public string PublishedDate { get; set; }

        public string Description { get; set; }

        public IndustryIdentifiers[] IndustryIdentifiers { get; set; }

        public ReadingModes ReadingModes { get; set; }

        public int PageCount { get; set; }

        public int PrintedPageCount { get; set; }

        public Dimensions Dimensions { get; set; }

        public string PrintType { get; set; }

        public string MaturityRating { get; set; }

        public bool AllowAnonLogging { get; set; }

        public string ContentVersion { get; set; }

        public PanelizationSummary PanelizationSummary { get; set; }

        public string Language { get; set; }

        public string PreviewLink { get; set; }

        public string InfoLink { get; set; }

        public string CanonicalVolumeLink { get; set; }
    }
}