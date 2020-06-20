namespace GoogleBooks.Client.Interfaces
{
    public interface IUrlFactory
    {
        /// <summary>
        /// Url read only property that must be previously set by using the "SetBooksCatalogUrl" method
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Sets a book details url from a book Id
        /// </summary>
        /// <param name="bookId"></param>
        void SetBookDetailsUrl(string bookId);

        /// <summary>
        /// Sets a books catalog url filtered by the keywords parameter with as many items per page as defined by the pageSize parameter and skipping every book item before the startIndex parameter
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageSize"></param>
        /// <param name="startIndex"></param>
        void SetBooksCatalogUrl(string keywords, int pageSize, int startIndex);
    }
}
