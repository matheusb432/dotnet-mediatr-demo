using DemoApp.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.Infra.Configurations.EntityConfigurations
{
    internal sealed class TodoItemConfiguration : BaseEntityConfiguration<TodoItem>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<TodoItem> builder)
        {
            builder.Property(t => t.Title).HasMaxLength(200).IsUnicode(false).IsRequired();
            builder
                .HasOne(t => t.TodoList)
                .WithMany(t => t.TodoItems)
                .HasForeignKey(t => t.TodoListId);
        }
    }
}
