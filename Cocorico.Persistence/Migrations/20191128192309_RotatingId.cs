using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocorico.Persistence.Migrations
{
    public partial class RotatingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RotatingId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RotatingId",
                table: "Orders");
        }
    }
}
