namespace GoogleBooks.Client.Dtos.Output
{
    public class BookDetailsFull : BookDetailsBase
    {
        public VolumeInfoFull VolumeInfo { get; set; }

        public SaleInfoLite SaleInfo { get; set; }

        public AccessInfo AccessInfo { get; set; }
    }
}
