using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DemoApp.Domain.Models;

namespace DemoApp.Infra.Configurations.EntityConfigurations
{
    internal sealed class TimesheetConfiguration : BaseEntityConfiguration<Timesheet>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<Timesheet> builder)
        {
            builder.Property(x => x.Date).HasColumnType("date");
            builder
                .HasMany(x => x.Tasks)
                .WithOne(x => x.Timesheet)
                .HasForeignKey(x => x.TimesheetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
