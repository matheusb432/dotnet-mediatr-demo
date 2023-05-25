using DemoApp.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.Infra.Configurations.EntityConfigurations
{
    internal sealed class TodoListConfiguration : BaseEntityConfiguration<TodoList>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<TodoList> builder)
        {
            builder.Property(t => t.Title).HasMaxLength(200).IsUnicode(false).IsRequired();
        }
    }
}
