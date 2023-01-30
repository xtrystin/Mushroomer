using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Entity;

namespace Post.Infrastructure.EF.Config.WritingConfig;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(typeof(string), "_content")
            .HasColumnType("varchar(200)")
            .HasColumnName("Content");

        builder.Property(typeof(DateTime), "_createdDate")
            .HasColumnName("CreatedDate")
            .HasColumnType("timestamp without time zone");

        builder.Property(typeof(DateTime), "_lastModificationDate")
            .HasColumnName("LastModificationDate")
            .HasColumnType("timestamp without time zone");

        builder.HasOne(p => p.Post)
            .WithMany("_comments")
            .HasForeignKey(p => p.PostId);

        builder.Property<Guid>("AuthorId")
            .HasColumnName("AuthorId")
            .HasColumnType("uuid");
        builder.HasOne("_author")
            .WithMany("_comments")
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey("AuthorId");

        builder.ToTable("Comment");
        //todo: declare foreignKey
    }
}
