using GoogleBooks.Client.Configuration;
using GoogleBooks.Client.Configuration.ConfigurationOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleBooks.Api.Configuration
{
    public static class ServicesConfiguration
    {
        public static void ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Api configuration
            services.Configure<GoogleBooksUrlOptions>(configuration.GetSection("Urls"));
            services.AddSingleton(typeof(IConfiguration), configuration);

            // Google Books Client configuration
            services.ConfigureGoogleBooksClientServices(configuration);
        }
    }
}
