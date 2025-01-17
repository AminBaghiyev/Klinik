using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Klinik.DL.Migrations
{
    /// <inheritdoc />
    public partial class ThumbnailAdded2Doctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPath",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailPath",
                table: "Doctors");
        }
    }
}
