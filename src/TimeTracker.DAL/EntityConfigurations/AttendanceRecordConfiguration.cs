using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.EntityConfigurations;

public class AttendanceRecordConfiguration: IEntityTypeConfiguration<AttendanceRecordEntity>
{
    public void Configure(EntityTypeBuilder<AttendanceRecordEntity> builder)
    {
        builder.ToTable("AttendanceRecords");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CheckIn)
            .IsRequired();

        builder.HasIndex(x => new { x.UserId, Date = x.AttendanceDate })
            .IsUnique();

        builder.HasOne(x => x.User)
            .WithMany(x => x.AttendanceRecords)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}