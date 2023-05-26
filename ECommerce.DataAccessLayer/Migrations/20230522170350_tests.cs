using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class tests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCart_Carts_CartId",
                table: "ItemCart");

            migrationBuilder.DropIndex(
                name: "IX_ItemCart_CartId",
                table: "ItemCart");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "ItemCart");

            migrationBuilder.AddColumn<int>(
                name: "ItemCartId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemCart_Id",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ItemCartId",
                table: "Carts",
                column: "ItemCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ItemCart_ItemCartId",
                table: "Carts",
                column: "ItemCartId",
                principalTable: "ItemCart",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ItemCart_ItemCartId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ItemCartId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ItemCartId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ItemCart_Id",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "ItemCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemCart_CartId",
                table: "ItemCart",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCart_Carts_CartId",
                table: "ItemCart",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
