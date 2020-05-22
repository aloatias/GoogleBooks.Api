﻿using GoogleBooks.Client.Configuration.ConfigurationOptions;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.Options;

namespace GoogleBooks.Client.Factories
{
    public class UrlFactory : IUrlFactory
    {
        private readonly GoogleBooksUrlOptions _options;

        public UrlFactory(IOptions<GoogleBooksUrlOptions> configuration)
        {
            _options = configuration.Value;
        }

        public string GetSearchDefaultsBooksUrl(string keywords)
        {
            return $"{_options.SearchDefaultBooks }{ keywords }";
        }
    }
}