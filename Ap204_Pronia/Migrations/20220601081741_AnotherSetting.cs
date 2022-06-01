using Microsoft.EntityFrameworkCore.Migrations;

namespace Ap204_Pronia.Migrations
{
    public partial class AnotherSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnotherSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderLogo = table.Column<string>(nullable: true),
                    FooterLogo = table.Column<string>(nullable: true),
                    SearchIcon = table.Column<string>(nullable: true),
                    AccountIcon = table.Column<string>(nullable: true),
                    WishListIcon = table.Column<string>(nullable: true),
                    BasketIcon = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    AdvertisementImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnotherSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnotherSettings");
        }
    }
}
