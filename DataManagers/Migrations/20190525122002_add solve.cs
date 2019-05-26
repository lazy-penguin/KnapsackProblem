using Microsoft.EntityFrameworkCore.Migrations;

namespace DataManagers.Migrations
{
    public partial class addsolve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "K",
                table: "Solves",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "W",
                table: "Solves",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "K",
                table: "Solves");

            migrationBuilder.DropColumn(
                name: "W",
                table: "Solves");
        }
    }
}
