using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WantList.Data.Migrations
{
    public partial class EpisodeCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EpisodeCount",
                table: "Animes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Animes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeCount",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Animes");
        }
    }
}
