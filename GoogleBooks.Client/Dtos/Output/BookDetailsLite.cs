namespace GoogleBooks.Client.Dtos.Output
{
    public class BookDetailsLite : BookDetailsBase
    {
        public VolumeInfoLite VolumeInfo { get; set; }

        public SaleInfoFull SaleInfo { get; set; }

        public AccessInfo AccessInfo { get; set; }
    }
}
