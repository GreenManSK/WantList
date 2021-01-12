using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace WantList.Data.Migrations
{
    public partial class Manga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MangaUpdatesId = table.Column<int>(nullable: false),
                    AddedDateTime = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    WantRank = table.Column<int>(nullable: false),
                    MissingVolumes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_MangaUpdatesId",
                table: "Mangas",
                column: "MangaUpdatesId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mangas");
        }
    }
}
