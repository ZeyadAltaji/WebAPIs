using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class part3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "SubProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_productId",
                table: "SubProducts",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_Products_productId",
                table: "SubProducts",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_Products_productId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_SubProducts_productId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "SubProducts");
        }
    }
}
