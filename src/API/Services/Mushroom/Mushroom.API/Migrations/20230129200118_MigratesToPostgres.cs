using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mushroom.API.Migrations
{
    /// <inheritdoc />
    public partial class MigratesToPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mushroom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsPoisonous = table.Column<bool>(type: "boolean", nullable: false),
                    PhotoUrl = table.Column<string>(type: "varchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mushroom", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mushroom");
        }
    }
}
