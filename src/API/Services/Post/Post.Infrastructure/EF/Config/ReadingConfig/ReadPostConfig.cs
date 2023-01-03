using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Application.Dto;
using Post.Application.ReadModel;

namespace Post.Infrastructure.EF.Config.ReadingConfig;

public class ReadPostConfig : IEntityTypeConfiguration<PostReadModel>
{
    public void Configure(EntityTypeBuilder<PostReadModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(x => x.Title)
            .HasColumnName("Title");

        builder.Property(x => x.Content)
            .HasColumnName("Content");

        builder.Property(x => x.CreatedDate)
            .HasColumnName("CreatedDate");

        builder.Property(x => x.LastModificationDate)
            .HasColumnName("LaastModificationDate");

        builder.HasMany(x => x.Comments)
            .WithOne();
        
        builder.HasOne(x => x.Author)
            .WithMany("_posts");

        builder.HasMany(x => x.Reactions);

        builder.ToTable("Post");
    }
}