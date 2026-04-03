using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    public partial class FixAdminPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Обновляем пароль администратора на корректный HMAC-SHA512 хэш для 'Admin123!'
                UPDATE ""Users"" SET
                    ""PasswordHash"" = decode('91E57CD7601798286180E0FB2513B4AF1BD49700EAE532D297AE7185A18096449C7C3A11C02DF3422740839477AD4F724D610715A84F84439435A480FF887E28', 'hex'),
                    ""PasswordSalt"" = decode('BF2063FA3F456913D15BA3DBD912D90E3BBF4E02712F76C885AB25AC329DC9FCC9B25EE52CFEB0190E1D568EB3D5041D5DB5A98FF08E54B45100ED92B3C2CC35CB40A368949ED979131AF5C8971DE7A339DDAEFE5398281B3D8081F27A4390C8CDC36D2A5B8EE745D64C8F200EE0CE9F4D75E3FE27AACCF14EA9C4362B0ECA48', 'hex')
                WHERE ""Email"" = 'admin@kraeved.ru';
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Восстановление невозможно
        }
    }
}
