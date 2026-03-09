using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KraevedAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangesForGeoAndEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageObjects_HistoricalEvents_HistoricalEventId",
                table: "ImageObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageObjects_ImageObjects_iconId",
                table: "ImageObjects");

            migrationBuilder.DropIndex(
                name: "IX_ImageObjects_HistoricalEventId",
                table: "ImageObjects");

            migrationBuilder.DropIndex(
                name: "IX_ImageObjects_iconId",
                table: "ImageObjects");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "ImageObjects");

            migrationBuilder.DropColumn(
                name: "HistoricalEventId",
                table: "ImageObjects");

            migrationBuilder.DropColumn(
                name: "iconId",
                table: "ImageObjects");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ImageObjects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "ImageObjects",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ImageObjects",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "HistoricalEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "HistoricalEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "GeoObjects",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "GeoObjects",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "ImageObjects");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ImageObjects");

            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "HistoricalEvents");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "HistoricalEvents");

            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "GeoObjects");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "GeoObjects");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ImageObjects",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "ImageObjects",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "HistoricalEventId",
                table: "ImageObjects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iconId",
                table: "ImageObjects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageObjects_HistoricalEventId",
                table: "ImageObjects",
                column: "HistoricalEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageObjects_iconId",
                table: "ImageObjects",
                column: "iconId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageObjects_HistoricalEvents_HistoricalEventId",
                table: "ImageObjects",
                column: "HistoricalEventId",
                principalTable: "HistoricalEvents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageObjects_ImageObjects_iconId",
                table: "ImageObjects",
                column: "iconId",
                principalTable: "ImageObjects",
                principalColumn: "Id");
        }
    }
}
