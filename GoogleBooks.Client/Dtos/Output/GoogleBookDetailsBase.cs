﻿namespace GoogleBooks.Client.Dtos.Output
{
    public abstract class GoogleBookDetailsBase
    {
        public string Id { get; set; }

        public string Kind { get; set; }

        public string Etag { get; set; }

        public string SelfLink { get; set; }
    }
}