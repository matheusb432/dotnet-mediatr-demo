using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;
using DemoApp.Domain.Models;
using DemoApp.Infra.Utils;

namespace DemoApp.Infra
{
    public sealed class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; } = null!;
        public DbSet<Timesheet> Timesheets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureDeleteBehavior(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetAssembly(typeof(TaskManagerContext))!
            );
        }

        private void ConfigureDeleteBehavior(ModelBuilder modelBuilder)
        {
            foreach (
                var relationship in modelBuilder.Model
                    .GetEntityTypes()
                    .SelectMany(e => e.GetForeignKeys())
            )
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
