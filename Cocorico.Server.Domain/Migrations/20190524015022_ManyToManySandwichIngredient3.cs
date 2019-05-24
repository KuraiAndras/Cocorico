using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocorico.Server.Domain.Migrations
{
    public partial class ManyToManySandwichIngredient3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sandwiches_Orders_OrderId",
                table: "Sandwiches");

            migrationBuilder.DropIndex(
                name: "IX_Sandwiches_OrderId",
                table: "Sandwiches");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Sandwiches");

            migrationBuilder.CreateTable(
                name: "SandwichOrder",
                columns: table => new
                {
                    SandwichId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SandwichOrder", x => new { x.SandwichId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_SandwichOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SandwichOrder_Sandwiches_SandwichId",
                        column: x => x.SandwichId,
                        principalTable: "Sandwiches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SandwichOrder_OrderId",
                table: "SandwichOrder",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SandwichOrder");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Sandwiches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sandwiches_OrderId",
                table: "Sandwiches",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sandwiches_Orders_OrderId",
                table: "Sandwiches",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
