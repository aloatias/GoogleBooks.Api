namespace GoogleBooks.Client.Dtos
{
    public class BookDetailsLite
    {
        public string Id { get; set; }

        public string Kind { get; set; }

        public string Etag { get; set; }

        public string SelfLink { get; set; }

        public VolumeInfoLite VolumeInfo { get; set; }

        public SaleInfoFull SaleInfo { get; set; }
    }
}
