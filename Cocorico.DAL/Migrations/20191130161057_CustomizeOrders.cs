using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocorico.DAL.Migrations
{
    public partial class CustomizeOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngredientModification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(nullable: false),
                    SandwichOrderId = table.Column<int>(nullable: false),
                    Modification = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientModification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientModification_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientModification_SandwichOrder_SandwichOrderId",
                        column: x => x.SandwichOrderId,
                        principalTable: "SandwichOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientModification_IngredientId",
                table: "IngredientModification",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientModification_SandwichOrderId",
                table: "IngredientModification",
                column: "SandwichOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientModification");
        }
    }
}
