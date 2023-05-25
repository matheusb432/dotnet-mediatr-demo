using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DemoApp.Application.Configurations
{
    public static class ApplicationConfig
    {
        public static void AddApplicationConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );
        }
    }
}
