using KraevedAPI.Models;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KraevedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeoObjectsController : ControllerBase
    {
        private readonly IKraevedService _kraevedService;
        public GeoObjectsController(IKraevedService kraevedService)
        {
            _kraevedService = kraevedService;
        }

        /// <summary>
        /// Получить гео-объект по индектификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GeoObject?>> GetGeoObjectById(int id)
        {
            GeoObject? result = null;
            try
            {
                result = await _kraevedService.GetGeoObjectById(id);

                if (result != null)
                {
                    if (result.Parent != null)
                    {
                        result.Parent = new GeoObject
                        {
                            Id = result.Parent.Id,
                            Name = result.Parent.Name,
                            TypeId = result.Parent.TypeId,
                            Type = result.Parent.Type != null ? new GeoObjectType
                            {
                                Id = result.Parent.Type.Id,
                                Name = result.Parent.Type.Name,
                                Title = result.Parent.Type.Title,
                            } : null,
                            ShortDescription = result.Parent.ShortDescription,
                            Thumbnail = result.Parent.Thumbnail,
                        };
                    }

                    if (result.Children != null)
                    {
                        result.Children = result.Children.Select(c => new GeoObject
                        {
                            Id = c.Id,
                            Name = c.Name,
                            TypeId = c.TypeId,
                            Type = c.Type != null ? new GeoObjectType
                            {
                                Id = c.Type.Id,
                                Name = c.Type.Name,
                                Title = c.Type.Title,
                            } : null,
                            ShortDescription = c.ShortDescription,
                            Thumbnail = c.Thumbnail,
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить список гео-объектов по фильтру
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GeoObjectBrief>>> GetGeoObjects([FromQuery] string? name, [FromQuery] int? regionId)
        {
            IEnumerable<GeoObjectBrief>? result;
            var filter = new GeoObjectFilter() { Name = name, RegionId = regionId };

            try
            {
                result = await _kraevedService.GetGeoObjectsByFilter(filter);
            }

            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Добавить гео-объект в БД
        /// </summary>
        /// <param name="geoObject"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> InsertGeoObject(GeoObject geoObject)
        {
            GeoObject? result;

            try
            {
                result = await _kraevedService.InsertGeoObject(geoObject);
            }

            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Импортировать гео-объекты из JSON файла
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("import")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> ImportGeoObjectsFromJson([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "Файл не выбран или пуст" });
            }

            if (!file.FileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { error = "Файл должен иметь расширение .json" });
            }

            try
            {
                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
                };

                var geoObjects = System.Text.Json.JsonSerializer.Deserialize<List<GeoObject>>(json, options);

                if (geoObjects == null || geoObjects.Count == 0)
                {
                    return BadRequest(new { error = "JSON файл пуст или имеет неверный формат. Ожидается массив объектов." });
                }

                var results = new List<GeoObject>();
                var errors = new List<string>();

                foreach (var geoObject in geoObjects)
                {
                    try
                    {
                        var result = await _kraevedService.InsertGeoObject(geoObject);
                        results.Add(result);
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Ошибка при импорте '{geoObject.Name}': {ex.Message}");
                    }
                }

                return Ok(new
                {
                    Imported = results.Count,
                    Failed = errors.Count,
                    Errors = errors.Count > 0 ? errors : null,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Ошибка обработки файла: {ex.Message}" });
            }
        }

        /// <summary>
        /// Удалить гео-объект по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteGeoObject(int id)
        {
            GeoObject? result = null;

            try
            {
                result = await _kraevedService.DeleteGeoObject(id);
            }

            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Изменить гео-объект
        /// </summary>
        /// <param name="geoObject"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> UpdateGeoObject([FromBody] GeoObject geoObject)
        {
            GeoObject? result = null;

            try
            {
                result = await _kraevedService.UpdateGeoObject(geoObject);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить персоны, привязанные к гео-объекту
        /// </summary>
        [HttpGet("{id}/persons")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PersonBriefDto>>> GetPersonsByGeoObjectId(int id)
        {
            try
            {
                var result = await _kraevedService.GetPersonsByGeoObjectId(id);
                var dtos = result.Where(p => p != null).Select(p => new PersonBriefDto
                {
                    Id = p!.Id,
                    Surname = p!.Surname,
                    FirstName = p!.FirstName,
                    Patronymic = p!.Patronymic,
                    BirthDate = p!.BirthDate,
                    DeathDate = p!.DeathDate,
                    Photos = p!.Photos?.Select(img => new ImageInfoDto { Id = img.Id, Filename = img.Filename, Caption = img.Caption }).ToList(),
                });
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
