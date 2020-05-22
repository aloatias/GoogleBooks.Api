namespace GoogleBooks.Client.Dtos
{
    public class BookDetail
    {
        public string Id { get; set; }

        public string Etag { get; set; }

        public string SelfLink { get; set; }

        public VolumeInfo VolumeInfo { get; set; }

        public SaleInfo SaleInfo { get; set; }
    }
}
