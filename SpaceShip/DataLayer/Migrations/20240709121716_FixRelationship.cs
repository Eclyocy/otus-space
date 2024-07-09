using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourcesType_Resources_ResourceId",
                table: "ResourcesType");

            migrationBuilder.DropIndex(
                name: "IX_ResourcesType_ResourceId",
                table: "ResourcesType");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "ResourcesType");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "Problems");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceTypeId",
                table: "Resources",
                column: "ResourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ResourcesType_ResourceTypeId",
                table: "Resources",
                column: "ResourceTypeId",
                principalTable: "ResourcesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ResourcesType_ResourceTypeId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ResourceTypeId",
                table: "Resources");

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "ResourcesType",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "Problems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesType_ResourceId",
                table: "ResourcesType",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourcesType_Resources_ResourceId",
                table: "ResourcesType",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id");
        }
    }
}
