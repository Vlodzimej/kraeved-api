using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KraevedAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedHistoricalObjectAndImageObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricalEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    iconId = table.Column<int>(type: "INTEGER", nullable: true),
                    Content = table.Column<byte[]>(type: "bytea", nullable: false),
                    HistoricalEventId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageObjects_HistoricalEvents_HistoricalEventId",
                        column: x => x.HistoricalEventId,
                        principalTable: "HistoricalEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImageObjects_ImageObjects_iconId",
                        column: x => x.iconId,
                        principalTable: "ImageObjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageObjects_HistoricalEventId",
                table: "ImageObjects",
                column: "HistoricalEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageObjects_iconId",
                table: "ImageObjects",
                column: "iconId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageObjects");

            migrationBuilder.DropTable(
                name: "HistoricalEvents");
        }
    }
}
