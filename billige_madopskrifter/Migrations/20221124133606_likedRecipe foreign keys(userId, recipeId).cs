using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace billige_madopskrifter.Migrations
{
    public partial class likedRecipeforeignkeysuserIdrecipeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LikedRecipes_RecipeId",
                table: "LikedRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedRecipes_UserId",
                table: "LikedRecipes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikedRecipes_Recipes_RecipeId",
                table: "LikedRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikedRecipes_Users_UserId",
                table: "LikedRecipes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedRecipes_Recipes_RecipeId",
                table: "LikedRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_LikedRecipes_Users_UserId",
                table: "LikedRecipes");

            migrationBuilder.DropIndex(
                name: "IX_LikedRecipes_RecipeId",
                table: "LikedRecipes");

            migrationBuilder.DropIndex(
                name: "IX_LikedRecipes_UserId",
                table: "LikedRecipes");
        }
    }
}
