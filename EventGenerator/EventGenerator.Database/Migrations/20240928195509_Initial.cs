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
            migrationBuilder.RenameColumn(
                name: "troublecoint",
                table: "generator",
                newName: "trouble_coins");

            migrationBuilder.RenameColumn(
                name: "genertator_id",
                table: "event",
                newName: "generator_id");

            migrationBuilder.RenameColumn(
                name: "event_coint",
                table: "event",
                newName: "event_level");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "trouble_coins",
                table: "generator",
                newName: "troublecoint");

            migrationBuilder.RenameColumn(
                name: "generator_id",
                table: "event",
                newName: "genertator_id");

            migrationBuilder.RenameColumn(
                name: "event_level",
                table: "event",
                newName: "event_coint");
        }
    }
}
