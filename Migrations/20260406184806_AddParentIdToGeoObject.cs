using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class AddParentIdToGeoObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "GeoObjects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeoObjects_ParentId",
                table: "GeoObjects",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoObjects_GeoObjects_ParentId",
                table: "GeoObjects",
                column: "ParentId",
                principalTable: "GeoObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoObjects_GeoObjects_ParentId",
                table: "GeoObjects");

            migrationBuilder.DropIndex(
                name: "IX_GeoObjects_ParentId",
                table: "GeoObjects");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "GeoObjects");
        }
    }
}
