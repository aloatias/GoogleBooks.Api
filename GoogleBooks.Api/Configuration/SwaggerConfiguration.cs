using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace GoogleBooks.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Google Books API",
                    Description = "A simple example of a ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Alvaro Atias",
                        Email = "aloatias88@gmail.com",
                        Url = new Uri("https://github.com/aloatias"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under Google APIs",
                        Url = new Uri("https://developers.google.com/books"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
