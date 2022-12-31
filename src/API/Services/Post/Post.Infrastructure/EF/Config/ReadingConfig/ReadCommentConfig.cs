using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Application.Dto;

namespace Post.Infrastructure.EF.Config.ReadingConfig;

public class ReadCommentConfig
{
    public void Configure(EntityTypeBuilder<CommentReadModel> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(x => x.Content)
            .HasColumnType("nvarchar(200)")
            .HasColumnName("Content");

        builder.Property(x => x.CreatedDate)
            .HasColumnName("CreatedDate");

        builder.Property(x => x.LastModificationDate)
            .HasColumnName("LastModificationDate");

        builder.HasOne(p => p.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(p => p.PostId);

        builder.HasOne(x => x.Author)
            .WithMany("_comments")
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("Comment");
        //todo: declare foreignKey
    }
}
