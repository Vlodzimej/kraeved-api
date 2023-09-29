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
            var geoObject = await _kraevedService.getGeoObjectById(id);

            if(geoObject == null) {
                return BadRequest();
            }

            return Ok(geoObject);
        }

        /// <summary>
        /// Получить список гео-объектов по идентификатору региона
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        [HttpGet("region/{regionId}")]
        public async Task<ActionResult<IEnumerable<GeoObject>>> GetGeoObjects(int regionId) 
        {
            var geoObjects = await _kraevedService.getGeoObjectsByRegionId(regionId);

            return Ok(geoObjects);
        }

        /// <summary>
        /// Добавить гео-объект в БД
        /// </summary>
        /// <param name="geoObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> InsertGeoObject(GeoObject geoObject)
        {
            await _kraevedService.insertGeoObject(geoObject);

            return Ok();
        }
    }
}
