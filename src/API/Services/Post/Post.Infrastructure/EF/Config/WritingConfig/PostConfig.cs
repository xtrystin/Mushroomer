using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Post.Domain.Entity;
using Post.Domain.ValueObject;

namespace Post.Infrastructure.EF.Config.WritingConfig;

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
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(typeof(PostTitle), "_title")
            .HasConversion(titleConvverter)
            .HasColumnName("Title")
        .HasColumnType("varchar(200)");

        builder.Property(typeof(PostContent), "_content")
            .HasConversion(contentConvverter)
            .HasColumnName("Content")
            .HasColumnType("text");

        builder.Property(typeof(DateTime), "_createdDate")
            .HasColumnName("CreatedDate")
            .HasColumnType("timestamp without time zone");

        builder.Property(typeof(DateTime), "_lastModificationDate")
            .HasColumnName("LaastModificationDate")
            .HasColumnType("timestamp without time zone");

        builder.HasMany(typeof(Comment), "_comments");

        builder.Property<Guid>("AuthorId")
            .HasColumnName("AuthorId")
            .HasColumnType("uuid");
        builder.HasOne("_author")
            .WithMany("_posts")
            .HasForeignKey("AuthorId");

        builder.HasMany(typeof(PostUserReaction), "_reactions");

        builder.ToTable("Post");
    }
}