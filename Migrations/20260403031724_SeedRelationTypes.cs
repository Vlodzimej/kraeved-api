using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    public partial class SeedRelationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert relation types with their paired types
            // We need to insert first, then update PairedTypeId
            migrationBuilder.InsertData(
                table: "PersonRelationTypes",
                columns: new[] { "Id", "Title", "Name" },
                values: new object[,]
                {
                    { 1, "Отец", "father" },
                    { 2, "Мать", "mother" },
                    { 3, "Сын", "son" },
                    { 4, "Дочь", "daughter" },
                    { 5, "Брат", "brother" },
                    { 6, "Сестра", "sister" },
                    { 7, "Муж", "husband" },
                    { 8, "Жена", "wife" },
                    { 9, "Дед", "grandfather" },
                    { 10, "Бабушка", "grandmother" },
                    { 11, "Внук", "grandson" },
                    { 12, "Внучка", "granddaughter" },
                }
            );

            // Set paired types: Father<->Son/Daughter, Mother<->Son/Daughter, Brother<->Sister, Husband<->Wife, Grandfather<->Grandson/Granddaughter
            migrationBuilder.Sql(@"
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 3 WHERE ""Id"" = 1;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 4 WHERE ""Id"" = 2;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 5 WHERE ""Id"" = 3;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 6 WHERE ""Id"" = 4;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 6 WHERE ""Id"" = 5;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 5 WHERE ""Id"" = 6;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 8 WHERE ""Id"" = 7;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 7 WHERE ""Id"" = 8;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 11 WHERE ""Id"" = 9;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 12 WHERE ""Id"" = 10;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 9 WHERE ""Id"" = 11;
                UPDATE ""PersonRelationTypes"" SET ""PairedTypeId"" = 10 WHERE ""Id"" = 12;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 1);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 2);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 3);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 4);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 5);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 6);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 7);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 8);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 9);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 10);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 11);
            migrationBuilder.DeleteData("PersonRelationTypes", "Id", 12);
        }
    }
}
