using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventGenerator.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "generator",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ship_id = table.Column<Guid>(type: "uuid", nullable: false),
                    trouble_coins = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generator", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    generator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_generator_generator_id",
                        column: x => x.generator_id,
                        principalTable: "generator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_generator_id",
                table: "event",
                column: "generator_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event");

            migrationBuilder.DropTable(
                name: "generator");
        }
    }
}
