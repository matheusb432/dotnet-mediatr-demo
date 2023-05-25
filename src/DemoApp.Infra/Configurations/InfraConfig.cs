using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DemoApp.Infra.Repositories;
using DemoApp.Infra.Utils;

namespace DemoApp.Infra.Configurations
{
    public static class InfraConfig
    {
        public static void AddInfraConfiguration(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDatabase(configuration);

            services.AddRepositories();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            services.AddScoped<ITimesheetRepository, TimesheetRepository>();
        }

        private static void AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration
        ) =>
            services.AddDbContext<TaskManagerContext>(
                opt =>
                    opt.UseSqlServer(
                            configuration.GetConnectionString(InfraUtils.DefaultConnectionName)
                        )
                        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Error)
            );
    }
}
