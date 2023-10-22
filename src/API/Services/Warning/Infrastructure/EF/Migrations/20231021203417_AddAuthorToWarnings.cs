using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorToWarnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Warning",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Warning_AuthorId",
                table: "Warning",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warning_User_AuthorId",
                table: "Warning",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warning_User_AuthorId",
                table: "Warning");

            migrationBuilder.DropIndex(
                name: "IX_Warning_AuthorId",
                table: "Warning");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Warning");
        }
    }
}
