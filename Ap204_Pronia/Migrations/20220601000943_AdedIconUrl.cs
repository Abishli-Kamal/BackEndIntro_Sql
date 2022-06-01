using Microsoft.EntityFrameworkCore.Migrations;

namespace Ap204_Pronia.Migrations
{
    public partial class AdedIconUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Seettings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Seettings");
        }
    }
}
