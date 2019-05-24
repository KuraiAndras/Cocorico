using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocorico.Server.Domain.Migrations
{
    public partial class ManyToManySandwichIngredient2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Sandwiches_SandwichId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_SandwichId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "SandwichId",
                table: "Ingredients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SandwichId",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_SandwichId",
                table: "Ingredients",
                column: "SandwichId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Sandwiches_SandwichId",
                table: "Ingredients",
                column: "SandwichId",
                principalTable: "Sandwiches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
