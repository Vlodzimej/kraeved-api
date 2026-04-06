using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class AddOKNCategoryAndTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""GeoObjectCategories"" (""Id"", ""Name"", ""Title"") VALUES
                (6, 'okn', 'Объекты культурного наследия')
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"") VALUES
                (601, 'MONUMENT', 'Памятник', 6),
                (602, 'ENSEMBLE', 'Ансамбль', 6),
                (603, 'NOTABLE_PLACE', 'Достопримечательное место', 6)
                ON CONFLICT (""Id"") DO NOTHING;

                ALTER SEQUENCE ""GeoObjectCategories_Id_seq"" RESTART WITH 7;
                ALTER SEQUENCE ""GeoObjectType_Id_seq"" RESTART WITH 604;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM ""GeoObjectTypes"" WHERE ""Id"" IN (601, 602, 603);
                DELETE FROM ""GeoObjectCategories"" WHERE ""Id"" = 6;

                ALTER SEQUENCE ""GeoObjectCategories_Id_seq"" RESTART WITH 6;
                ALTER SEQUENCE ""GeoObjectType_Id_seq"" RESTART WITH 506;
            ");
        }
    }
}
