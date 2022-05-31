using Microsoft.EntityFrameworkCore.Migrations;

namespace Ap204_Pronia.Migrations
{
    public partial class PlantCategoriesDeleteNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantCategories_Categories_CategoryId",
                table: "PlantCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantCategories_Plants_PlantId",
                table: "PlantCategories");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "PlantCategories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "PlantCategories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCategories_Categories_CategoryId",
                table: "PlantCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCategories_Plants_PlantId",
                table: "PlantCategories",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantCategories_Categories_CategoryId",
                table: "PlantCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantCategories_Plants_PlantId",
                table: "PlantCategories");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "PlantCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "PlantCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCategories_Categories_CategoryId",
                table: "PlantCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCategories_Plants_PlantId",
                table: "PlantCategories",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
