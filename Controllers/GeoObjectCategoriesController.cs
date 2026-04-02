using KraevedAPI.Models;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KraevedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeoObjectCategoriesController : ControllerBase
    {
        private readonly IKraevedService _kraevedService;

        public GeoObjectCategoriesController(IKraevedService kraevedService)
        {
            _kraevedService = kraevedService;
        }

        /// <summary>
        /// Получить категорию гео-объекта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Категория гео-объекта</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GeoObjectCategory?>> GetGeoObjectCategoryById(int id)
        {
            GeoObjectCategory? result = null;
            try
            {
                result = await _kraevedService.GetGeoObjectCategoryById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить список категорий гео-объектов
        /// </summary>
        /// <returns>Список категорий</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GeoObjectCategory>>> GetAllGeoObjectCategories()
        {
            IEnumerable<GeoObjectCategory>? result;

            try
            {
                result = await _kraevedService.GetAllGeoObjectCategories();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Добавить категорию гео-объекта
        /// </summary>
        /// <param name="geoObjectCategory">Категория гео-объекта</param>
        /// <returns>Созданная категория</returns>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> InsertGeoObjectCategory(GeoObjectCategory geoObjectCategory)
        {
            GeoObjectCategory? result = null;

            try
            {
                result = await _kraevedService.InsertGeoObjectCategory(geoObjectCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Удалить категорию гео-объекта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Удалённая категория</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteGeoObjectCategory(int id)
        {
            GeoObjectCategory? result = null;

            try
            {
                result = await _kraevedService.DeleteGeoObjectCategory(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Обновить категорию гео-объекта
        /// </summary>
        /// <param name="geoObjectCategory">Категория гео-объекта</param>
        /// <returns>Обновлённая категория</returns>
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> UpdateGeoObjectCategory([FromBody] GeoObjectCategory geoObjectCategory)
        {
            GeoObjectCategory? result = null;

            try
            {
                result = await _kraevedService.UpdateGeoObjectCategory(geoObjectCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }
    }
}
