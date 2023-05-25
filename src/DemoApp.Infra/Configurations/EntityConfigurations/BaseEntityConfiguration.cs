using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DemoApp.Domain.Models;

namespace DemoApp.Infra.Configurations.EntityConfigurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            ConfigureOtherProperties(builder);
        }

        public abstract void ConfigureOtherProperties(EntityTypeBuilder<T> builder);
    }
}
