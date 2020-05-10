namespace GoogleBooks.Client.Dtos
{
    public class SaleInfo
    {
        public string Country { get; set; }

        public string Saleability { get; set; }

        public bool IsEbook { get; set; }

        public ListPrice ListPrice { get; set; }
    }
}