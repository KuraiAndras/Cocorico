using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocorico.Persistence.Migrations
{
    public partial class OpeningOnOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpeningId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OpeningId",
                table: "Orders",
                column: "OpeningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Openings_OpeningId",
                table: "Orders",
                column: "OpeningId",
                principalTable: "Openings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Openings_OpeningId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OpeningId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OpeningId",
                table: "Orders");
        }
    }
}
