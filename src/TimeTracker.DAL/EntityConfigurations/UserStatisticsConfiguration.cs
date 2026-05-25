using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.EntityConfigurations;

public class UserStatisticsConfiguration : IEntityTypeConfiguration<UserStatisticsEntity>
{
    public void Configure(EntityTypeBuilder<UserStatisticsEntity> builder)
    {
        builder.ToTable("UserStatistics");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .ValueGeneratedNever();

        builder.Property(x => x.ExpectedWork)
            .IsRequired();

        builder.Property(x => x.Worked)
            .IsRequired();

        builder.Property(x => x.Missing)
            .IsRequired();

        builder.Property(x => x.LateCount)
            .IsRequired();

        builder.Property(x => x.EarlyLeaveCount)
            .IsRequired();

        builder.Property(x => x.LateWithReason)
            .IsRequired();

        builder.Property(x => x.LateWithoutReason)
            .IsRequired();

        builder.Property(x => x.EarlyWithReason)
            .IsRequired();

        builder.Property(x => x.EarlyWithoutReason)
            .IsRequired();
    }
}