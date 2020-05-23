namespace GoogleBooks.Client.Dtos
{
    public class BookDetailsFull
    {
        public string Id { get; set; }

        public string Kind { get; set; }

        public string Etag { get; set; }

        public string SelfLink { get; set; }

        public VolumeInfoFull VolumeInfo { get; set; }

        public SaleInfoLite SaleInfo { get; set; }

        public AccessInfo AccessInfo { get; set; }
    }
}
