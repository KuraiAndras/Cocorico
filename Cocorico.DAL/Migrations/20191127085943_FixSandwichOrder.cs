using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocorico.DAL.Migrations
{
    public partial class FixSandwichOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SandwichOrder",
                table: "SandwichOrder");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SandwichOrder",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SandwichOrder",
                table: "SandwichOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SandwichOrder_SandwichId",
                table: "SandwichOrder",
                column: "SandwichId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SandwichOrder",
                table: "SandwichOrder");

            migrationBuilder.DropIndex(
                name: "IX_SandwichOrder_SandwichId",
                table: "SandwichOrder");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SandwichOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SandwichOrder",
                table: "SandwichOrder",
                columns: new[] { "SandwichId", "OrderId" });
        }
    }
}
