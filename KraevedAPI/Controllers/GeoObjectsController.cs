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

        [HttpGet("{id}")]
        public async Task<ActionResult<GeoObject>> GetGeoObjectById(int id)
        {
            var geoObject = await _kraevedService.getGeoObjectById(id);

            if(geoObject == null) {
                return BadRequest();
            }

            return Ok(geoObject);
        }

        [HttpGet("region/{regionId}")]
        public async Task<ActionResult<IEnumerable<GeoObject>>> GetGeoObjects(int regionId) 
        {
            var geoObjects = await _kraevedService.getGeoObjectsByRegionId(regionId);

            return Ok(geoObjects);
        }

        [HttpPost]
        public async Task<ActionResult> InsertGeoObject(GeoObject geoObject)
        {
            await _kraevedService.insertGeoObject(geoObject);

            return Ok();
        }

    }
}
