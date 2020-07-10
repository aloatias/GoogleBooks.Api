namespace GoogleBooks.Client.Dtos.Output
{
    public abstract class SaleInfoBase
    {
        public string Country { get; set; }

        public string Saleability { get; set; }

        public bool IsEbook { get; set; }
    }
}