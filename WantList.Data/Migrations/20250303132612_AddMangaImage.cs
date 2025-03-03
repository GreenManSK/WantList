using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WantList.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMangaImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Mangas",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Mangas");
        }
    }
}
