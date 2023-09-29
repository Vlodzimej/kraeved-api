using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KraevedAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedGeoObjectRegionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "GeoObjects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "GeoObjects");
        }
    }
}
