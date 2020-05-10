using GoogleBooks.Client.Configuration;
using GoogleBooks.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleBooks.Client.Integration.Tests
{
    public class TestFactory
    {
        protected IGoogleBooksClientService CreateGoogleBooksClientService()
        {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = ServicesConfiguration.ConfigureGoogleBooksClientServices(serviceCollection);

            return serviceProvider.GetRequiredService<IGoogleBooksClientService>();
        }
    }
}