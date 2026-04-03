using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    public partial class ClearTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Очистка связанных таблиц (порядок важен из-за FK)
                DELETE FROM ""PersonRelations"";
                DELETE FROM ""PersonGeoObjects"";
                DELETE FROM ""Comments"";
                DELETE FROM ""GeoObjects"";
                DELETE FROM ""Persons"";
                DELETE FROM ""GeoObjectTypes"";
                DELETE FROM ""GeoObjectCategories"";
                DELETE FROM ""HistoricalEvents"";
                DELETE FROM ""ImageObjects"";
                DELETE FROM ""SmsCodes"";
                DELETE FROM ""Users"" WHERE ""Email"" != 'admin@kraeved.ru';
                DELETE FROM ""AppSettings"";

                -- Сброс счётчиков автоинкремента
                ALTER SEQUENCE ""GeoObjects_Id_seq"" RESTART WITH 1;
                ALTER SEQUENCE ""Persons_Id_seq"" RESTART WITH 1;
                ALTER SEQUENCE ""GeoObjectTypes_Id_seq"" RESTART WITH 101;
                ALTER SEQUENCE ""GeoObjectCategories_Id_seq"" RESTART WITH 1;
                ALTER SEQUENCE ""HistoricalEvents_Id_seq"" RESTART WITH 1;
                ALTER SEQUENCE ""ImageObjects_Id_seq"" RESTART WITH 1;
                ALTER SEQUENCE ""Comments_Id_seq"" RESTART WITH 1;
                ALTER SEQUENCE ""Users_Id_seq"" RESTART WITH 2;
                ALTER SEQUENCE ""AppSettings_Id_seq"" RESTART WITH 1;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Восстановление невозможно — данные удалены безвозвратно
        }
    }
}
