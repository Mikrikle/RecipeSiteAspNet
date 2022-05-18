using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteAspNet.Data.Migrations
{
    public partial class FixRecipeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AuthorID",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_ImgID",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Recipes",
                newName: "RecipeSiteUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_AuthorID",
                table: "Recipes",
                newName: "IX_Recipes_RecipeSiteUserID");

            migrationBuilder.AlterColumn<int>(
                name: "ImgID",
                table: "Recipes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_RecipeSiteUserID",
                table: "Recipes",
                column: "RecipeSiteUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_ImgID",
                table: "Recipes",
                column: "ImgID",
                principalTable: "Images",
                principalColumn: "ImgID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_RecipeSiteUserID",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Images_ImgID",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "RecipeSiteUserID",
                table: "Recipes",
                newName: "AuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeSiteUserID",
                table: "Recipes",
                newName: "IX_Recipes_AuthorID");

            migrationBuilder.AlterColumn<int>(
                name: "ImgID",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AuthorID",
                table: "Recipes",
                column: "AuthorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Images_ImgID",
                table: "Recipes",
                column: "ImgID",
                principalTable: "Images",
                principalColumn: "ImgID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
