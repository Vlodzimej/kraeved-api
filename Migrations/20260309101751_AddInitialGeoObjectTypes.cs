using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialGeoObjectTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Историко-культурное наследие (CategoryId = 1)
            migrationBuilder.Sql($@"
                -- Мемориалы и памятники
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (101, 'MILITARY_MEMORIAL', 'Воинские мемориалы', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (102, 'PERSON_MONUMENT', 'Памятники историческим личностям', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (103, 'EVENT_MONUMENT', 'Памятники событиям', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (104, 'MEMORIAL_PLAQUE', 'Мемориальные доски', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (105, 'TECHNICAL_MONUMENT', 'Памятники труду и технике', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (106, 'SCULPTURE', 'Скульптурные композиции', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                -- Культовая архитектура
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (111, 'ORTHODOX_CHURCH', 'Православные храмы и церкви', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (112, 'MONASTERY', 'Монастыри', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (113, 'CHAPEL', 'Часовни', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (114, 'HOLY_SPRING', 'Святые источники', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                -- Усадебная архитектура
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (121, 'MANOR', 'Дворянские усадьбы', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (122, 'MANOR_PARK', 'Усадебные парки', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                -- Гражданская архитектура
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (131, 'HISTORICAL_BUILDING', 'Исторические здания', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (132, 'MERCHANT_HOUSE', 'Купеческие дома', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (133, 'INDUSTRIAL_OBJECT', 'Промышленные объекты (старые заводы, фабрики)', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (134, 'WATER_TOWER', 'Водонапорные башни', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (135, 'BRIDGE', 'Мосты', 1)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (136, 'RAILWAY_STATION', 'Железнодорожные станции и вокзалы', 1)
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Природные объекты (CategoryId = 2)
            migrationBuilder.Sql($@"
                -- Водные объекты
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (201, 'RIVER', 'Реки', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (202, 'LAKE', 'Озера (в т.ч. старицы)', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (203, 'POND', 'Пруды', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (204, 'SPRING', 'Родники и ключи', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                -- Рельеф
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (211, 'RAVINE', 'Овраги и балки', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (212, 'HILL', 'Холмы и возвышенности', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (213, 'VALLEY', 'Долины рек', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                -- Специфические объекты
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (221, 'QUARRY', 'Карьеры (старые разработки)', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (222, 'CAVE', 'Пещеры (искусственные и естественные)', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                -- Биологические объекты
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (231, 'OLD_TREE', 'Старовозрастные деревья', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (232, 'GROVE', 'Рощи', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (233, 'ALLEY', 'Аллеи', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (234, 'PARK', 'Городские парки и скверы', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                -- ООПТ
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (241, 'NATIONAL_PARK', 'Национальные парки (Угра)', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (242, 'NATURE_RESERVE', 'Природные заказники', 2)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (243, 'NATURAL_MONUMENT', 'Памятники природы', 2)
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Археологические объекты (CategoryId = 3)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (301, 'ANCIENT_SETTLEMENT', 'Древние стоянки', 3)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (302, 'HILLFORT', 'Городища', 3)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (303, 'BURIAL_MOUND', 'Курганы', 3)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (304, 'ANCIENT_SETTLEMENT_SITE', 'Селища', 3)
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Топонимические объекты (CategoryId = 4)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (401, 'CITY', 'Города', 4)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (402, 'VILLAGE', 'Села и деревни', 4)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (403, 'TRACT', 'Урочища (исчезнувшие деревни)', 4)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (404, 'VIEWPOINT', 'Смотровые площадки', 4)
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Инфраструктурные объекты (CategoryId = 5)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (501, 'TOURIST_ROUTE', 'Туристические маршруты', 5)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (502, 'TOURIST_CAMP', 'Туристические стоянки', 5)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (503, 'MUSEUM', 'Музеи', 5)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (504, 'VISITOR_CENTER', 'Визит-центры', 5)
                ON CONFLICT (""Id"") DO NOTHING;

                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"")
                VALUES (505, 'ECOLOGICAL_TRAIL', 'Экологические тропы', 5)
                ON CONFLICT (""Id"") DO NOTHING;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаление всех типов объектов (диапазоны Id: 101-599)
            migrationBuilder.Sql($@"
                DELETE FROM ""GeoObjectTypes""
                WHERE ""Id"" BETWEEN 101 AND 599
            ");
        }
    }
}