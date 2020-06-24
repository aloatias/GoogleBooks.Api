namespace GoogleBooks.Client.Dtos.Output
{
    public class GoogleBookDetailsFull : GoogleBookDetailsBase
    {
        public VolumeInfoFull VolumeInfo { get; set; }

        public SaleInfoFull SaleInfo { get; set; }

        public AccessInfo AccessInfo { get; set; }
    }
}
