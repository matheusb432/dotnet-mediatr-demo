using DemoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DemoApp.Infra
{
    public sealed class DemoAppContext : DbContext
    {
        public DemoAppContext(DbContextOptions options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<TodoList> TodoLists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetAssembly(typeof(DemoAppContext))!
            );
        }
    }
}
