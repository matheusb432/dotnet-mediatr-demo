using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace DemoApp.Application.Configurations
{
    public static class ApplicationConfig
    {
        public static void AddApplicationConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
