using Microsoft.EntityFrameworkCore.Migrations;
using KraevedAPI.Constants;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class RolesCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                INSERT INTO ""Roles"" (""Id"", ""Name"")
                VALUES ({Roles.Unknown.Id}, '{Roles.Unknown.Name}')
                ON CONFLICT (""Id"") DO NOTHING;
                
                INSERT INTO ""Roles"" (""Id"", ""Name"")
                VALUES ({Roles.Admin.Id}, '{Roles.Admin.Name}')
                ON CONFLICT (""Id"") DO NOTHING;
                
                INSERT INTO ""Roles"" (""Id"", ""Name"")
                VALUES ({Roles.User.Id}, '{Roles.User.Name}')
                ON CONFLICT (""Id"") DO NOTHING;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                DELETE FROM ""Roles""
                WHERE ""Id"" IN ({Roles.Unknown.Id}, {Roles.Admin.Id}, {Roles.User.Id})
            ");
        }
    }
}
