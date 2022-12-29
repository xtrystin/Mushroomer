﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Entity;

namespace Post.Infrastructure.EF.Config;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(typeof(string), "_content")
            .HasColumnType("nvarchar(200)")
            .HasColumnName("Content");

        builder.Property(typeof(DateTime), "_createdDate")
            .HasColumnName("CreatedDate");

        builder.Property(typeof(DateTime), "_lastModificationDate")
            .HasColumnName("LastModificationDate");

        builder.HasOne(p => p.Post)
            .WithMany("_comments")
            .HasForeignKey(p => p.PostId);

        builder.ToTable("Comment");
        //todo: declare foreignKey
    }
}
