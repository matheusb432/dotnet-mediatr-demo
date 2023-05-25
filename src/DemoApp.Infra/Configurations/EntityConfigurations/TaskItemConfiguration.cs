﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DemoApp.Domain.Models;

namespace DemoApp.Infra.Configurations.EntityConfigurations
{
    internal sealed class TaskItemConfiguration : BaseEntityConfiguration<TaskItem>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<TaskItem> builder)
        {
            builder.Property(x => x.Title).IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.Importance).HasDefaultValue(1);
            builder.Property(x => x.Comment).IsUnicode(false).IsRequired(false);
            builder
                .HasOne(x => x.Timesheet)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.TimesheetId);
        }
    }
}