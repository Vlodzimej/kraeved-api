using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonRelationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PairedTypeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonRelationTypes_PersonRelationTypes_PairedTypeId",
                        column: x => x.PairedTypeId,
                        principalTable: "PersonRelationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonRelations",
                columns: table => new
                {
                    PersonId1 = table.Column<int>(type: "integer", nullable: false),
                    PersonId2 = table.Column<int>(type: "integer", nullable: false),
                    RelationTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelations", x => new { x.PersonId1, x.PersonId2, x.RelationTypeId });
                    table.ForeignKey(
                        name: "FK_PersonRelations_PersonRelationTypes_RelationTypeId",
                        column: x => x.RelationTypeId,
                        principalTable: "PersonRelationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRelations_Persons_PersonId1",
                        column: x => x.PersonId1,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRelations_Persons_PersonId2",
                        column: x => x.PersonId2,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelationTypes_PairedTypeId",
                table: "PersonRelationTypes",
                column: "PairedTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_PersonId2",
                table: "PersonRelations",
                column: "PersonId2");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_RelationTypeId",
                table: "PersonRelations",
                column: "RelationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonRelations");

            migrationBuilder.DropTable(
                name: "PersonRelationTypes");
        }
    }
}
