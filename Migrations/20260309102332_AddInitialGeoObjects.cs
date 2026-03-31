using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kraeved.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialGeoObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Каменный мост (Id: 1001)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1001,
                    'Каменный мост',
                    135, -- bridge
                    'Каменный мост через Березуйский овраг — уникальное сооружение начала XIX века, один из символов Калуги. Построен в 1777-1778 годах по проекту архитектора Петра Никитина. Это один из старейших каменных виадуков в России. Длина моста составляет 160 метров, высота — около 20 метров. Мост соединяет центральную часть города с Загородным садом.',
                    'Уникальный каменный виадук XVIII века через Березуйский овраг, один из символов Калуги',
                    54.513056,
                    36.259444,
                    40,
                    '/images/geoobjects/kaluga/stone_bridge_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Гостиный двор (Id: 1002)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1002,
                    'Гостиный двор',
                    131, -- historical_building
                    'Гостиный двор в Калуге — архитектурный комплекс конца XVIII — начала XIX века, памятник федерального значения. Строительство велось по проекту архитектора Петра Никитина в 1784-1823 годах. Здание выполнено в стиле классицизма с элементами псевдоготики, имеет форму неправильного четырехугольника с внутренним двором и галереями. Является одним из крупнейших гостиных дворов России.',
                    'Архитектурный комплекс XVIII-XIX веков в стиле классицизма и псевдоготики',
                    54.512222,
                    36.261111,
                    40,
                    '/images/geoobjects/kaluga/gostiny_dvor_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Троицкий собор (Id: 1003)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1003,
                    'Свято-Троицкий кафедральный собор',
                    111, -- orthodox_church
                    'Троицкий собор — главный православный храм Калуги, построенный в 1786-1819 годах в стиле классицизма. Собор имеет величественную ротонду с куполом и колокольню. Внутри сохранились уникальные росписи и иконостас. Храм расположен на Соборной площади и является доминантой исторического центра города.',
                    'Главный православный храм Калуги в стиле классицизма (1786-1819)',
                    54.514444,
                    36.259167,
                    40,
                    '/images/geoobjects/kaluga/trinity_cathedral_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Церковь Космы и Дамиана (Id: 1004)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1004,
                    'Церковь Космы и Дамиана',
                    111, -- orthodox_church
                    'Церковь Космы и Дамиана — один из старейших храмов Калуги, построенный в 1794 году в стиле классицизма. Отличается необычной архитектурой с ротондой и колокольней. Храм известен тем, что в нем крестили основоположника космонавтики К.Э. Циолковского.',
                    'Старейший храм Калуги (1794), где крестили К.Э. Циолковского',
                    54.506389,
                    36.252778,
                    40,
                    '/images/geoobjects/kaluga/cosmas_damian_church_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Дом-музей Циолковского (Id: 1005)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1005,
                    'Дом-музей К.Э. Циолковского',
                    503, -- museum
                    'Мемориальный дом-музей основоположника космонавтики К.Э. Циолковского. Ученый жил в этом доме с 1904 по 1933 год. Здесь им были написаны десятки важнейших работ по теории космонавтики. Музей открыт в 1936 году и сохраняет подлинную обстановку, личные вещи и научные труды ученого.',
                    'Мемориальный дом, где жил и работал К.Э. Циолковский (1904-1933)',
                    54.511667,
                    36.245556,
                    40,
                    '/images/geoobjects/kaluga/tsiolkovsky_house_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Государственный музей истории космонавтики (Id: 1006)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1006,
                    'Государственный музей истории космонавтики им. К.Э. Циолковского',
                    503, -- museum
                    'Первый в мире и крупнейший в России музей космической тематики. Открыт в 1967 году при участии Ю.А. Гагарина и С.П. Королева. В музее представлена уникальная коллекция ракетно-космической техники, документов, личных вещей космонавтов. Современное здание с планетарием открыто после реконструкции в 2021 году.',
                    'Крупнейший в России музей космонавтики с уникальной коллекцией космической техники',
                    54.520000,
                    36.229722,
                    40,
                    '/images/geoobjects/kaluga/cosmonautics_museum_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Палаты Коробовых (Id: 1007)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1007,
                    'Палаты Коробовых',
                    131, -- historical_building
                    'Палаты Коробовых — памятник гражданской архитектуры XVII века, единственный образец каменного жилого здания допетровского времени в Калуге. Принадлежали купцу Коробову. Здание двухэтажное, выполнено из кирпича, с характерными для той эпохи элементами декора. В настоящее время является филиалом краеведческого музея.',
                    'Единственный в Калуге образец каменного жилого здания XVII века',
                    54.505556,
                    36.250556,
                    40,
                    '/images/geoobjects/kaluga/korobov_chambers_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Присутственные места (Id: 1008)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1008,
                    'Присутственные места',
                    131, -- historical_building
                    'Комплекс зданий Присутственных мест — памятник архитектуры конца XVIII века. Построен в 1780-1787 годах по проекту архитектора Петра Никитина в стиле классицизма. Здесь размещались губернские административные учреждения. В настоящее время здесь располагается Калужский государственный университет.',
                    'Административное здание конца XVIII века в стиле классицизма',
                    54.513611,
                    36.258056,
                    40,
                    '/images/geoobjects/kaluga/public_offices_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Церковь Георгия за верхом (Id: 1009)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1009,
                    'Церковь Георгия за верхом',
                    111, -- orthodox_church
                    'Церковь Георгия Победоносца «за верхом» построена в 1700-1701 годах на средства купцов Коробовых. Это один из немногих сохранившихся в Калуге памятников архитектуры XVII века в стиле «нарышкинского барокко». Характеризуется изящным декором, пятиглавием и шатровой колокольней.',
                    'Памятник архитектуры XVII века в стиле «нарышкинского барокко»',
                    54.508056,
                    36.254722,
                    40,
                    '/images/geoobjects/kaluga/st_george_church_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Памятник Циолковскому (Id: 1010)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1010,
                    'Памятник К.Э. Циолковскому',
                    102, -- person_monument
                    'Памятник основоположнику космонавтики установлен в 1958 году около музея истории космонавтики. Скульптор — А.П. Файдыш-Крандиевский, архитекторы — М.О. Барщ и А.Н. Колчин. Бронзовая фигура ученого изображена стоящей на фоне стелы, символизирующей космическую ракету.',
                    'Бронзовый памятник основоположнику космонавтики (1958)',
                    54.520278,
                    36.231111,
                    40,
                    '/images/geoobjects/kaluga/tsiolkovsky_monument_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Сквер Мира (Id: 1011)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1011,
                    'Сквер Мира (Загородный сад)',
                    234, -- park
                    'Загородный сад (ныне сквер Мира) — один из старейших парков Калуги, основанный в конце XVIII века. Расположен на высоком берегу Оки, примыкает к Каменному мосту. В парке сохранились вековые деревья, установлены памятники, есть смотровая площадка с видом на Оку и Заречье.',
                    'Старейший парк Калуги со смотровой площадкой на берегу Оки',
                    54.512778,
                    36.254444,
                    40,
                    '/images/geoobjects/kaluga/mir_park_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Смотровая площадка у Каменного моста (Id: 1012)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1012,
                    'Смотровая площадка у Каменного моста',
                    404, -- viewpoint
                    'Смотровая площадка расположена у Каменного моста со стороны сквера Мира. Открывается великолепный вид на Березуйский овраг, историческую застройку и панораму города. Особенно красиво здесь в вечернее время, когда включается подсветка моста.',
                    'Живописная смотровая площадка с видом на Березуйский овраг',
                    54.513333,
                    36.257778,
                    40,
                    '/images/geoobjects/kaluga/viewpoint_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Дом Щукина (Id: 1013)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1013,
                    'Дом Щукина (Дом мастеров)',
                    132, -- merchant_house
                    'Дом купца Щукина (XIX век) — яркий образец деревянного зодчества Калуги. Резной декор, мезонин, наличники характерны для купеческой архитектуры того времени. С 1990-х годов здесь размещается «Дом мастеров — центр народных промыслов и ремесел, где проводятся выставки и мастер-классы.',
                    'Образец деревянного купеческого зодчества XIX века, центр ремесел',
                    54.505833,
                    36.253056,
                    40,
                    '/images/geoobjects/kaluga/schukin_house_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Воинский мемориал на площади Победы (Id: 1014)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1014,
                    'Мемориальный комплекс на площади Победы',
                    101, -- military_memorial
                    'Мемориальный комплекс в честь воинов-калужан, погибших в годы Великой Отечественной войны. Открыт в 1966 году. Центральный элемент — 30-метровый обелиск. В комплекс входят Вечный огонь, стелы с именами Героев Советского Союза — уроженцев Калужской области, боевая техника времен войны.',
                    'Мемориал с Вечным огнем в память о воинах-калужанах',
                    54.524722,
                    36.269444,
                    40,
                    '/images/geoobjects/kaluga/war_memorial_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Усадьба Золотарёвых (Id: 1015)
            migrationBuilder.Sql($@"
                INSERT INTO ""GeoObjects"" (
                    ""Id"", ""Name"", ""TypeId"", ""Description"", ""ShortDescription"", 
                    ""Latitude"", ""Longitude"", ""RegionId"", ""Thumbnail""
                )
                VALUES (
                    1015,
                    'Усадьба Золотарёвых (Калужский областной художественный музей)',
                    121, -- manor
                    'Городская усадьба купцов Золотарёвых — памятник архитектуры начала XIX века. Главный дом выполнен в стиле классицизма с портиком. С 1919 года здесь размещается художественный музей с богатой коллекцией русского и западноевропейского искусства (картины, скульптура, фарфор).',
                    'Дворянская усадьба XIX века, ныне областной художественный музей',
                    54.511111,
                    36.254167,
                    40,
                    '/images/geoobjects/kaluga/zolotarev_estate_thumb.jpg'
                )
                ON CONFLICT (""Id"") DO NOTHING;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаление всех гео-объектов Калуги (Id: 1001-1015)
            migrationBuilder.Sql($@"
                DELETE FROM ""GeoObjects""
                WHERE ""Id"" BETWEEN 1001 AND 1015
            ");
        }
    }
}
