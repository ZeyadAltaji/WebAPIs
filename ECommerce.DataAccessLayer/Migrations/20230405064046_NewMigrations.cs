using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "Specials",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Order_Date",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "Coupons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specials_productId",
                table: "Specials",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_productId",
                table: "Coupons",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Products_productId",
                table: "Coupons",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specials_Products_productId",
                table: "Specials",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Products_productId",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_Specials_Products_productId",
                table: "Specials");

            migrationBuilder.DropIndex(
                name: "IX_Specials_productId",
                table: "Specials");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_productId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "Specials");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "Coupons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Order_Date",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
