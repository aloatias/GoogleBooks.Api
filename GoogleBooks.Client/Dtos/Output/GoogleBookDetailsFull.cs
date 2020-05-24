namespace GoogleBooks.Client.Dtos.Output
{
    public class GoogleBookDetailsFull : GoogleBookDetailsBase
    {
        public VolumeInfoFull VolumeInfo { get; set; }

        public SaleInfoLite SaleInfo { get; set; }

        public AccessInfo AccessInfo { get; set; }
    }
}
