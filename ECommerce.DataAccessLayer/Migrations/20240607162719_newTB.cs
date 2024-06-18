using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class newTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    MainCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArTxt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EnTxt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OTHTxt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OTHTxt1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.MainCode);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    CodeId = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MessageEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MessageOTHTxt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MessageOTHTxt1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.CodeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
