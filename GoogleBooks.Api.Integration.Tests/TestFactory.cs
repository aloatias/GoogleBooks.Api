using Microsoft.Extensions.Logging;
using Moq;

namespace GoogleBooks.Api.Integration.Tests
{
    public class TestFactory
    {
        protected ILogger<T> CreateLogger<T>()
        {
            return new LoggerFactory().CreateLogger<T>();
        }

        protected Mock<T> MockService<T>() where T : class
        {
            return new Mock<T>();
        }
    }
}
