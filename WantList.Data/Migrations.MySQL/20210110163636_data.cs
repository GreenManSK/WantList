using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace WantList.Data.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnidbAnimes",
                columns: table => new
                {
                    AnidbId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Japanese = table.Column<string>(nullable: true),
                    English = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnidbAnimes", x => x.AnidbId);
                });

            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    AnidbId = table.Column<int>(nullable: false),
                    AddedDateTime = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    WantRank = table.Column<int>(nullable: false),
                    Redownload = table.Column<bool>(nullable: false),
                    BluRay = table.Column<bool>(nullable: false),
                    Quality = table.Column<int>(nullable: false),
                    BluRayRelease = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnidbAnimes");

            migrationBuilder.DropTable(
                name: "Animes");
        }
    }
}
