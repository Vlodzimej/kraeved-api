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
    }
}
