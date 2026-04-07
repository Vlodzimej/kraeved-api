using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class AddSubtypeToGeoObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubtypeId",
                table: "GeoObjects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeoObjects_SubtypeId",
                table: "GeoObjects",
                column: "SubtypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoObjects_GeoObjectTypes_SubtypeId",
                table: "GeoObjects",
                column: "SubtypeId",
                principalTable: "GeoObjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoObjects_GeoObjectTypes_SubtypeId",
                table: "GeoObjects");

            migrationBuilder.DropIndex(
                name: "IX_GeoObjects_SubtypeId",
                table: "GeoObjects");

            migrationBuilder.DropColumn(
                name: "SubtypeId",
                table: "GeoObjects");
        }
    }
}
