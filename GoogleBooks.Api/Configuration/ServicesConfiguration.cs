using GoogleBooks.Api.Interfaces;
using GoogleBooks.Api.Services;
using GoogleBooks.Client.Configuration;
using GoogleBooks.Client.Configuration.ConfigurationOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleBooks.Api.Configuration
{
    public static class ServicesConfiguration
    {
        private const string _urlsSection = "Urls";

        public static void ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Api configuration
            services.AddScoped<IBooksService, BooksService>();
            services.Configure<GoogleBooksUrlOptions>(configuration.GetSection(_urlsSection));

            // Google Books Client configuration
            services.ConfigureGoogleBooksClientServices(configuration);
        }
    }
}
