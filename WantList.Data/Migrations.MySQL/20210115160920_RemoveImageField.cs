using Microsoft.EntityFrameworkCore.Migrations;

namespace WantList.Data.Migrations
{
    public partial class RemoveImageField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Animes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Mangas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Animes",
                type: "text",
                nullable: true);
        }
    }
}
