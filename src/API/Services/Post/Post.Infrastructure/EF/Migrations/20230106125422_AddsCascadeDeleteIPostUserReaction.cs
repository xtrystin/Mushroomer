using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Post.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddsCascadeDeleteIPostUserReaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUserReaction_Post_PostId",
                table: "PostUserReaction");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUserReaction_Post_PostId",
                table: "PostUserReaction",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUserReaction_Post_PostId",
                table: "PostUserReaction");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUserReaction_Post_PostId",
                table: "PostUserReaction",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }
    }
}
