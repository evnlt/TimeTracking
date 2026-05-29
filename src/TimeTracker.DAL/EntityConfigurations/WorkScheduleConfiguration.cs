using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.EntityConfigurations;

public class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkScheduleEntity>
{
    public void Configure(EntityTypeBuilder<WorkScheduleEntity> builder)
    {
        builder.ToTable("WorkSchedules");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.FreeSchedule)
            .IsRequired();

        builder.Property(x => x.WorkingDays)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                v => JsonSerializer.Deserialize<List<DayOfWeek>>(v, (JsonSerializerOptions)null!)!
            );
        
        builder.HasOne(x => x.User)
            .WithOne(x => x.WorkSchedule)
            .HasForeignKey<WorkScheduleEntity>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}