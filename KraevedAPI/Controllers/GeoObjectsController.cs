using KraevedAPI.ClassObjects;
using KraevedAPI.Core;
using KraevedAPI.Models;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KraevedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<GeoObject>> GetGeoObjectById(int id)
        {
            var result = await _kraevedService.getGeoObjectById(id);

            if(result == null) {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить список гео-объектов по фильру
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeoObjectBrief>>> GetGeoObjects([FromQuery] string? name, [FromQuery] int? regionId) 
        {
            var filter = new GeoObjectFilter() { Name = name, RegionId = regionId };
            var result = await _kraevedService.getGeoObjectsByFilter(filter);
            string? errorMessage = null;

            if (result == null) 
            {
                errorMessage = "Ошибка поиска";
            }

            return errorMessage != null ? BadRequest(new { errorMessage }) : Ok(result);
        }

        /// <summary>
        /// Добавить гео-объект в БД
        /// </summary>
        /// <param name="geoObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> InsertGeoObject(GeoObject geoObject)
        {
            var result = await _kraevedService.insertGeoObject(geoObject);

            if (result == null)
            {
                return BadRequest(new { message = "Объект не создан" });
            }

            return Ok(result);
        }

        /// <summary>
        /// Удалить гео-объект по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGeoObject(int id)
        {
            var result = await _kraevedService.deleteGeoObject(id);

            if(result == null) {
                return BadRequest(new { message = "Объект не найден" });
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateGeoObject([FromBody]GeoObject geoObject)
        {
            var result = await _kraevedService.updateGeoObject(geoObject);

            if (result == null)
            {
                return BadRequest(new { message = "Объект не найден" });
            }

            return Ok(result);
        }
    }
}
