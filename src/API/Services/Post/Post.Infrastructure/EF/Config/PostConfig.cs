using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Post.Domain.Entity;
using Post.Domain.ValueObject;

namespace Post.Infrastructure.EF.Config;

public class PostConfig : IEntityTypeConfiguration<Domain.Entity.Post>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Post> builder)
    {
        builder.HasKey(x => x.Id);

        var titleConvverter = new ValueConverter<PostTitle, string>(postTitle => postTitle.Value.ToString(),
            value => new PostTitle(value));

        var contentConvverter = new ValueConverter<PostContent, string>(postContent => postContent.Value.ToString(),
            value => new PostContent(value));

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, id => new PostId(id))
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(typeof(PostTitle), "_title")
            .HasConversion(titleConvverter)
            .HasColumnName("Title");
            //.HasColumnType("nvarchar(10000)") //todo: optimalization, now is nvarchar(unlimited)

        builder.Property(typeof(PostContent), "_content")
            .HasConversion(contentConvverter)
            .HasColumnName("Content");

        builder.Property(typeof(DateTime), "_createdDate")
            .HasColumnName("CreatedDate");

        builder.Property(typeof(DateTime), "_lastModificationDate")
            .HasColumnName("LaastModificationDate");

        builder.HasMany(typeof(Comment), "_comments");

        builder.ToTable("Post");
    }
}