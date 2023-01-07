using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatesWarningUserReactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /// USER TABLE IS MANAGED IN USER MICROSERVICE
            /// 
            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "WarningUserReaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Approve = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarningId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarningUserReaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarningUserReaction_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarningUserReaction_Warning_WarningId",
                        column: x => x.WarningId,
                        principalTable: "Warning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarningUserReaction_UserId",
                table: "WarningUserReaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarningUserReaction_WarningId",
                table: "WarningUserReaction",
                column: "WarningId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarningUserReaction");
            
            /// USER TABLE IS MANAGED IN USER MICROSERVICE
            /// 
            //migrationBuilder.DropTable(
            //    name: "User");
        }
    }
}
