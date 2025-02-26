using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrescentCoffee.Migrations
{
    /// <inheritdoc />
    public partial class changedImageColNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Coffees",
                newName: "ImageThumbnailPath");

            migrationBuilder.RenameColumn(
                name: "ImageThumbNailUrl",
                table: "Coffees",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageThumbnailPath",
                table: "Coffees",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Coffees",
                newName: "ImageThumbNailUrl");
        }
    }
}
