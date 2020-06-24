namespace GoogleBooks.Api.Dtos.Output
{
    public class BookDetailsForCatalog
    {
        public string Id { get; set; }
        
        public string Kind { get; set; }
        
        public string Etag { get; set; }

        public string SelfLink { get; set; }

        public string Title { get; set; }
        
        public string[] Authors { get; set; }
        
        public string Publisher { get; set; }

        public string PublishedDate { get; set; }

        public string Description { get; set; }

        public string TextReadingMode { get; set; }

        public bool? ImageReadingMode { get; set; }

        public int? PageCount { get; set; }

        public int? PrintedPageCount { get; set; }

        public string PrintType { get; set; }

        public string MaturityRating { get; set; }

        public bool? AllowAnonLogging { get; set; }

        public string ContentVersion { get; set; }

        public string Language { get; set; }

        public string PreviewLink { get; set; }

        public string InfoLink { get; set; }

        public string CanonicalVolumeLink { get; set; }

        public string SmallThumbNail { get; set; }
        
        public string Thumbnail { get; set; }
        
        public string Country { get; set; }

        public string Saleability { get; set; }
        
        public bool? IsEbook { get; set; }

        public bool? Embeddable { get; set; }

        public bool? PublicDomain { get; set; }

        public string TextToSpeechPermission { get; set; }
        
        public bool? IsPdfAvailable { get; set; }

        public string PdfActsTokenLink { get; set; }

        public string WebReaderLink { get; set; }

        public string AccessViewStatus { get; set; }

        public string QuoteSharingAllowed { get; set; }

        public string[] Categories { get; set; }
    }
}