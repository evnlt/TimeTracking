using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.EntityConfigurations;

public class ScheduleExclusionConfiguration : IEntityTypeConfiguration<ScheduleExclusionEntity>
{
    public void Configure(EntityTypeBuilder<ScheduleExclusionEntity> builder)
    {
        builder.ToTable("ScheduleExclusions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.StartDateTime)
            .IsRequired();

        builder.Property(x => x.EndDateTime)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.ScheduleExclusions)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}