using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class UppercaseGeoObjectCategoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE ""GeoObjectCategories"" SET ""Name"" = UPPER(""Name"");
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE ""GeoObjectCategories"" SET ""Name"" = LOWER(""Name"");
            ");
        }
    }
}
