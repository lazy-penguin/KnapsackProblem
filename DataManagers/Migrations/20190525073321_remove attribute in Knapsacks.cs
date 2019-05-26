using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataManagers.Migrations
{
    public partial class removeattributeinKnapsacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Knapsacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knapsacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Table = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Percent = table.Column<int>(nullable: false),
                    MaxValue = table.Column<int>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    KnapsackId = table.Column<int>(nullable: false),
                    SolveId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Knapsacks_KnapsackId",
                        column: x => x.KnapsackId,
                        principalTable: "Knapsacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Solves_SolveId",
                        column: x => x.SolveId,
                        principalTable: "Solves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Weight = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    KnapsackId = table.Column<int>(nullable: true),
                    KnapsackTaskId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Knapsacks_KnapsackId",
                        column: x => x.KnapsackId,
                        principalTable: "Knapsacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Tasks_KnapsackTaskId",
                        column: x => x.KnapsackTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_KnapsackId",
                table: "Items",
                column: "KnapsackId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_KnapsackTaskId",
                table: "Items",
                column: "KnapsackTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_KnapsackId",
                table: "Tasks",
                column: "KnapsackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SolveId",
                table: "Tasks",
                column: "SolveId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Knapsacks");

            migrationBuilder.DropTable(
                name: "Solves");
        }
    }
}
