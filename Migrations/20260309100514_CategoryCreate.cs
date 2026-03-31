using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class CategoryCreate: Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoObjects_GeoObjectType_TypeId",
                table: "GeoObjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeoObjectType",
                table: "GeoObjectType");

            migrationBuilder.RenameTable(
                name: "GeoObjectType",
                newName: "GeoObjectTypes");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "GeoObjectTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeoObjectTypes",
                table: "GeoObjectTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GeoObjectCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoObjectCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoObjectTypes_CategoryId",
                table: "GeoObjectTypes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoObjectTypes_GeoObjectCategories_CategoryId",
                table: "GeoObjectTypes",
                column: "CategoryId",
                principalTable: "GeoObjectCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoObjects_GeoObjectTypes_TypeId",
                table: "GeoObjects",
                column: "TypeId",
                principalTable: "GeoObjectTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoObjectTypes_GeoObjectCategories_CategoryId",
                table: "GeoObjectTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoObjects_GeoObjectTypes_TypeId",
                table: "GeoObjects");

            migrationBuilder.DropTable(
                name: "GeoObjectCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeoObjectTypes",
                table: "GeoObjectTypes");

            migrationBuilder.DropIndex(
                name: "IX_GeoObjectTypes_CategoryId",
                table: "GeoObjectTypes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "GeoObjectTypes");

            migrationBuilder.RenameTable(
                name: "GeoObjectTypes",
                newName: "GeoObjectType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeoObjectType",
                table: "GeoObjectType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoObjects_GeoObjectType_TypeId",
                table: "GeoObjects",
                column: "TypeId",
                principalTable: "GeoObjectType",
                principalColumn: "Id");
        }
    }
}
