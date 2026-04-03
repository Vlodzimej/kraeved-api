using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    public partial class SeedRelationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"PersonRelationTypes\";");

            migrationBuilder.InsertData(
                table: "PersonRelationTypes",
                columns: new[] { "Id", "Title", "Name" },
                values: new object[,]
                {
                    { 1, "Родитель", "parent" },
                    { 2, "Ребёнок", "child" },
                    { 3, "Брат/Сестра", "sibling" },
                    { 4, "Супруг", "spouse" },
                    { 5, "Дед", "grandparent" },
                    { 6, "Внук", "grandchild" },
                }
            );

            migrationBuilder.Sql(@"
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 2 WHERE ""Id"" = 1;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 1 WHERE ""Id"" = 2;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 3 WHERE ""Id"" = 3;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 4 WHERE ""Id"" = 4;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 6 WHERE ""Id"" = 5;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 5 WHERE ""Id"" = 6;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"PersonRelationTypes\";");
        }
    }
}
