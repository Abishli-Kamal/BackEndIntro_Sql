using Microsoft.EntityFrameworkCore.Migrations;

namespace Ap204_Pronia.Migrations
{
    public partial class AdedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeettingId",
                table: "SocialMedias",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_SeettingId",
                table: "SocialMedias",
                column: "SeettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedias_Seettings_SeettingId",
                table: "SocialMedias",
                column: "SeettingId",
                principalTable: "Seettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedias_Seettings_SeettingId",
                table: "SocialMedias");

            migrationBuilder.DropIndex(
                name: "IX_SocialMedias_SeettingId",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "SeettingId",
                table: "SocialMedias");
        }
    }
}
