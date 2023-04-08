using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class newtesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Photos",
                newName: "sub_Image2Url");

            migrationBuilder.AddColumn<string>(
                name: "main_ImageUrl",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sub_Image1Url",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "main_ImageUrl",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "sub_Image1Url",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "sub_Image2Url",
                table: "Photos",
                newName: "ImageUrl");
        }
    }
}
