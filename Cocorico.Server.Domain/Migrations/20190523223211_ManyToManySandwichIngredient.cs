using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocorico.Server.Domain.Migrations
{
    public partial class ManyToManySandwichIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SandwichIngredient",
                columns: table => new
                {
                    SandwichId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SandwichIngredient", x => new { x.SandwichId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_SandwichIngredient_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SandwichIngredient_Sandwiches_SandwichId",
                        column: x => x.SandwichId,
                        principalTable: "Sandwiches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SandwichIngredient_IngredientId",
                table: "SandwichIngredient",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SandwichIngredient");
        }
    }
}
