using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.EntityConfigurations;

public class CardAssignmentConfiguration : IEntityTypeConfiguration<CardEntity>
{
    public void Configure(EntityTypeBuilder<CardEntity> builder)
    {
        builder.ToTable("Cards");

        builder.HasKey(x => x.CardUid);

        builder.Property(x => x.CardUid)
            .HasMaxLength(128);

        builder.Property(x => x.AssignedAt)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Cards)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}