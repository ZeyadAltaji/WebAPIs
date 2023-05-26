using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class newTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SProducts_productId",
                table: "SProducts",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_SProducts_Products_productId",
                table: "SProducts",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SProducts_Products_productId",
                table: "SProducts");

            migrationBuilder.DropIndex(
                name: "IX_SProducts_productId",
                table: "SProducts");
        }
    }
}
