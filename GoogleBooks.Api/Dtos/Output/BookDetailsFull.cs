namespace GoogleBooks.Api.Dtos.Output
{
    public class BookDetailsFull
    {
        public string Id { get; set; }

        public string Etag { get; set; }

        public string Title { get; set; }

        public string[] Authors { get; set; }

        public string Publisher { get; set; }

        public string PublishedDate { get; set; }

        public string Description { get; set; }

        public int PageCount { get; set; }

        public int PrintedPageCount { get; set; }

        public Dimensions Dimensions { get; set; }

        public string PrintType { get; set; }

        public string Language { get; set; }

        public string PreviewLink { get; set; }

        public string InfoLink { get; set; }

        public string CanonicalVolumeLink { get; set; }

        public string SmallImage { get; set; }

        public string MediumImage { get; set; }

        public string LargeImage { get; set; }

        public string ExtraLargeImage { get; set; }

        public string Country { get; set; }

        public string Saleability { get; set; }

        public bool IsEbook { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; }

        public string Viewability { get; set; }

        public bool IsPdfAvailable { get; set; }

        public string PdfActsTokenLink { get; set; }

        public string WebReaderLink { get; set; }
        
        public string SmallThumbnail { get; set; }

        public string Thumbnail { get; set; }
        
        public bool Embeddable { get; set; }

        public bool PublicDomain { get; set; }
        
        public string AccessViewStatus { get; set; }

        public string QuoteSharingAllowed { get; set; }
    }
}