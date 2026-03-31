using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialGeoObjectCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Историко-культурное наследие
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjectCategories"" (""Id"", ""Name"", ""Title"")
                VALUES (1, 'historical_cultural', 'Историко-культурное наследие')
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Природные объекты
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjectCategories"" (""Id"", ""Name"", ""Title"")
                VALUES (2, 'natural', 'Природные объекты')
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Археологические объекты
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjectCategories"" (""Id"", ""Name"", ""Title"")
                VALUES (3, 'archaeological', 'Археологические объекты')
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Топонимические объекты
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjectCategories"" (""Id"", ""Name"", ""Title"")
                VALUES (4, 'toponymic', 'Топонимические и пространственные ориентиры')
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Инфраструктурные объекты
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjectCategories"" (""Id"", ""Name"", ""Title"")
                VALUES (5, 'infrastructure', 'Инфраструктурные и туристические объекты')
                ON CONFLICT (""Id"") DO NOTHING;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                DELETE FROM ""GeoObjectCategories""
                WHERE ""Id"" BETWEEN 1 AND 5
            ");
        }
    }
}
