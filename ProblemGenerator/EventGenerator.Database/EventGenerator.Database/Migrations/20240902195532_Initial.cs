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
            migrationBuilder.DropTable(
                name: "ship");

            migrationBuilder.RenameColumn(
                name: "troublecoint",
                table: "event",
                newName: "event_coint");

            migrationBuilder.RenameColumn(
                name: "ship_id",
                table: "event",
                newName: "genertator_id");

            migrationBuilder.CreateTable(
                name: "generator",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    generator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ship_id = table.Column<Guid>(type: "uuid", nullable: false),
                    troublecoint = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generator", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "generator");

            migrationBuilder.RenameColumn(
                name: "genertator_id",
                table: "event",
                newName: "ship_id");

            migrationBuilder.RenameColumn(
                name: "event_coint",
                table: "event",
                newName: "troublecoint");

            migrationBuilder.CreateTable(
                name: "ship",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ship_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ship", x => x.id);
                });
        }
    }
}
