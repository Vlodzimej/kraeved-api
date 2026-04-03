using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    public partial class SeedProductionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Категории гео-объектов
                INSERT INTO ""GeoObjectCategories"" (""Id"", ""Name"", ""Title"") VALUES
                (1, 'historical_cultural', 'Историко-культурное наследие'),
                (2, 'natural', 'Природные объекты'),
                (3, 'archaeological', 'Археологические объекты'),
                (4, 'toponymic', 'Топонимические и пространственные ориентиры'),
                (5, 'infrastructure', 'Инфраструктурные и туристические объекты')
                ON CONFLICT (""Id"") DO NOTHING;

                -- Типы гео-объектов
                INSERT INTO ""GeoObjectTypes"" (""Id"", ""Name"", ""Title"", ""CategoryId"") VALUES
                -- Историко-культурное наследие
                (101, 'MILITARY_MEMORIAL', 'Воинские мемориалы', 1),
                (102, 'PERSON_MONUMENT', 'Памятники историческим личностям', 1),
                (103, 'EVENT_MONUMENT', 'Памятники событиям', 1),
                (104, 'MEMORIAL_PLAQUE', 'Мемориальные доски', 1),
                (105, 'TECHNICAL_MONUMENT', 'Памятники труду и технике', 1),
                (106, 'SCULPTURE', 'Скульптурные композиции', 1),
                (111, 'ORTHODOX_CHURCH', 'Православные храмы и церкви', 1),
                (112, 'MONASTERY', 'Монастыри', 1),
                (113, 'CHAPEL', 'Часовни', 1),
                (114, 'HOLY_SPRING', 'Святые источники', 1),
                (121, 'MANOR', 'Дворянские усадьбы', 1),
                (122, 'MANOR_PARK', 'Усадебные парки', 1),
                (131, 'HISTORICAL_BUILDING', 'Исторические здания', 1),
                (132, 'MERCHANT_HOUSE', 'Купеческие дома', 1),
                (133, 'INDUSTRIAL_OBJECT', 'Промышленные объекты', 1),
                (134, 'WATER_TOWER', 'Водонапорные башни', 1),
                (135, 'BRIDGE', 'Мосты', 1),
                (136, 'RAILWAY_STATION', 'Железнодорожные станции и вокзалы', 1),
                -- Природные объекты
                (201, 'RIVER', 'Реки', 2),
                (202, 'LAKE', 'Озера', 2),
                (203, 'POND', 'Пруды', 2),
                (204, 'SPRING', 'Родники и ключи', 2),
                (211, 'RAVINE', 'Овраги и балки', 2),
                (212, 'HILL', 'Холмы и возвышенности', 2),
                (213, 'VALLEY', 'Долины рек', 2),
                (221, 'QUARRY', 'Карьеры', 2),
                (222, 'CAVE', 'Пещеры', 2),
                (231, 'OLD_TREE', 'Старовозрастные деревья', 2),
                (232, 'GROVE', 'Рощи', 2),
                (233, 'ALLEY', 'Аллеи', 2),
                (234, 'PARK', 'Городские парки и скверы', 2),
                (241, 'NATIONAL_PARK', 'Национальные парки', 2),
                (242, 'NATURE_RESERVE', 'Природные заказники', 2),
                (243, 'NATURAL_MONUMENT', 'Памятники природы', 2),
                -- Археологические объекты
                (301, 'ANCIENT_SETTLEMENT', 'Древние стоянки', 3),
                (302, 'HILLFORT', 'Городища', 3),
                (303, 'BURIAL_MOUND', 'Курганы', 3),
                (304, 'ANCIENT_SETTLEMENT_SITE', 'Селища', 3),
                -- Топонимические ориентиры
                (401, 'CITY', 'Города', 4),
                (402, 'VILLAGE', 'Села и деревни', 4),
                (403, 'TRACT', 'Урочища', 4),
                (404, 'VIEWPOINT', 'Смотровые площадки', 4),
                -- Инфраструктурные объекты
                (501, 'TOURIST_ROUTE', 'Туристические маршруты', 5),
                (502, 'TOURIST_CAMP', 'Туристические стоянки', 5),
                (503, 'MUSEUM', 'Музеи', 5),
                (504, 'VISITOR_CENTER', 'Визит-центры', 5),
                (505, 'ECOLOGICAL_TRAIL', 'Экологические тропы', 5)
                ON CONFLICT (""Id"") DO NOTHING;

                -- Типы родственных связей
                INSERT INTO ""PersonRelationTypes"" (""Id"", ""Title"", ""Name"", ""PairedTypeId"") VALUES
                (1, 'Родитель', 'parent', 2),
                (2, 'Ребёнок', 'child', 1),
                (3, 'Брат/Сестра', 'sibling', 3),
                (4, 'Супруг', 'spouse', 4),
                (5, 'Дед', 'grandparent', 6),
                (6, 'Внук', 'grandchild', 5)
                ON CONFLICT (""Id"") DO NOTHING;

                -- Администратор по умолчанию (пароль: Admin123!)
                INSERT INTO ""Users"" (""Id"", ""Phone"", ""Email"", ""Name"", ""Surname"", ""PasswordHash"", ""PasswordSalt"", ""StartDate"", ""RoleId"")
                VALUES (
                    1,
                    '',
                    'admin@kraeved.ru',
                    'Администратор',
                    '',
                    decode('8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918', 'hex'),
                    decode('', 'hex'),
                    NOW(),
                    1
                )
                ON CONFLICT (""Id"") DO NOTHING;

                -- Сброс счётчиков
                ALTER SEQUENCE ""GeoObjectType_Id_seq"" RESTART WITH 506;
                ALTER SEQUENCE ""GeoObjectCategories_Id_seq"" RESTART WITH 6;
                ALTER SEQUENCE ""PersonRelationTypes_Id_seq"" RESTART WITH 7;
                ALTER SEQUENCE ""Users_Id_seq"" RESTART WITH 2;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Восстановление невозможно
        }
    }
}
