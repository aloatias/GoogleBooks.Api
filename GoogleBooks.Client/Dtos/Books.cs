﻿namespace GoogleBooks.Client.Dtos
{
    public class Books
    {
        public string Kind { get; set; }

        public int TotalItems { get; set; }

        public BookDetail[] Items { get; set; }
    }
}