// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Post.Infrastructure.EF.Context;

#nullable disable

namespace Post.Infrastructure.Migrations
{
    [DbContext(typeof(PostDbContext))]
    [Migration("20230106125422_AddsCascadeDeleteIPostUserReaction")]
    partial class AddsCascadeDeleteIPostUserReaction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Post.Domain.Entity.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AuthorId");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("_content")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Content");

                    b.Property<DateTime>("_createdDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime>("_lastModificationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastModificationDate");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("Post.Domain.Entity.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AuthorId");

                    b.Property<string>("_content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Content");

                    b.Property<DateTime>("_createdDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime>("_lastModificationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("LaastModificationDate");

                    b.Property<string>("_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("Post.Domain.Entity.PostUserReaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Like")
                        .HasColumnType("bit")
                        .HasColumnName("Like");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PostId");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostUserReaction", (string)null);
                });

            modelBuilder.Entity("Post.Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EmailAddress");

                    b.Property<string>("_firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstName");

                    b.Property<string>("_lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastName");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Post.Domain.Entity.Comment", b =>
                {
                    b.HasOne("Post.Domain.Entity.User", "_author")
                        .WithMany("_comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Post.Domain.Entity.Post", "Post")
                        .WithMany("_comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("_author");
                });

            modelBuilder.Entity("Post.Domain.Entity.Post", b =>
                {
                    b.HasOne("Post.Domain.Entity.User", "_author")
                        .WithMany("_posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_author");
                });

            modelBuilder.Entity("Post.Domain.Entity.PostUserReaction", b =>
                {
                    b.HasOne("Post.Domain.Entity.Post", "Post")
                        .WithMany("_reactions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Post.Domain.Entity.User", "User")
                        .WithMany("_postReactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Post.Domain.Entity.Post", b =>
                {
                    b.Navigation("_comments");

                    b.Navigation("_reactions");
                });

            modelBuilder.Entity("Post.Domain.Entity.User", b =>
                {
                    b.Navigation("_comments");

                    b.Navigation("_postReactions");

                    b.Navigation("_posts");
                });
#pragma warning restore 612, 618
        }
    }
}
