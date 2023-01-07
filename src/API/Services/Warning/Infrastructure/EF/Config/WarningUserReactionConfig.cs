﻿using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Config;

public class WarningUserReactionConfig : IEntityTypeConfiguration<WarningUserReaction>
{
    public void Configure(EntityTypeBuilder<WarningUserReaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Approve)
            .IsRequired()
            .HasColumnName("Approve");

        builder.Property(x => x.UserId)
            .HasColumnName("UserId");

        builder.Property(x => x.WarningId)
            .HasColumnName("WarningId");

        builder.HasOne(x => x.Warning)
            .WithMany("_reactions")
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(x => x.WarningId);

        builder.HasOne(x => x.User)
            .WithMany("_warningReactions")
            .OnDelete(DeleteBehavior.NoAction)  //todo cascade?
            .HasForeignKey(x => x.UserId);

        builder.ToTable("WarningUserReaction");
    }
}