namespace GoogleBooks.Client.Dtos
{
    public class BookDetailsLite : BookDetailsBase
    {
        public VolumeInfoLite VolumeInfo { get; set; }

        public SaleInfoFull SaleInfo { get; set; }

        public AccessInfo AccessInfo { get; set; }
    }
}
