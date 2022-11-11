using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace billige_madopskrifter.Migrations
{
    public partial class AddedestimatedPricetoRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EstimatedPrice",
                table: "Recipes",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedPrice",
                table: "Recipes");
        }
    }
}
