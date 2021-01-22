using Microsoft.EntityFrameworkCore.Migrations;

namespace WantList.Data.Migrations
{
    public partial class DeletedFieldManga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Mangas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Mangas");
        }
    }
}
