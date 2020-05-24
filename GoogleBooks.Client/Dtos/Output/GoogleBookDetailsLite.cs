namespace GoogleBooks.Client.Dtos.Output
{
    public class GoogleBookDetailsLite : GoogleBookDetailsBase
    {
        public VolumeInfoLite VolumeInfo { get; set; }

        public SaleInfoFull SaleInfo { get; set; }

        public AccessInfo AccessInfo { get; set; }
    }
}
