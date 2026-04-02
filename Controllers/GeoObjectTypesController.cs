using KraevedAPI.Models;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KraevedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeoObjectTypesController : ControllerBase
    {
        private readonly IKraevedService _kraevedService;
        public GeoObjectTypesController(IKraevedService kraevedService)
        {
            _kraevedService = kraevedService;
        }

        /// <summary>
        /// Получить тип гео-объекта по индектификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GeoObjectType?>> GetGeoObjectTypeById(int id)
        {
            GeoObjectType? result = null;
            try {
                result = await _kraevedService.GetGeoObjectTypeById(id);
            }
            catch(Exception ex) {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        /// <summary>
        /// Получить список типов гео-объектов по фильтру
        /// </summary>
        /// <param name="name"></param>
        /// <param name="regionId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GeoObjectType>>> GetAllGeoObjectTypes() 
        {
            IEnumerable<GeoObjectType>? result;

            try {
                result = await _kraevedService.GetAllGeoObjectTypes();
            }

            catch(Exception ex) {
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
        public async Task<ActionResult> InsertGeoObjectType(GeoObjectType geoObjectType)
        {
            GeoObjectType? result = null;

            try {
                result = await _kraevedService.InsertGeoObjectType(geoObjectType);
            }

            catch(Exception ex) {
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
        public async Task<ActionResult> DeleteGeoObjectType(int id)
        {
            GeoObjectType? result = null;

            try {
                result = await _kraevedService.DeleteGeoObjectType(id);
            }

            catch(Exception ex) {
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
        public async Task<ActionResult> UpdateGeoObjectType([FromBody]GeoObjectType geoObjectType)
        {
            GeoObjectType? result = null;

            try {
                result = await _kraevedService.UpdateGeoObjectType(geoObjectType);
            }
            catch(Exception ex) {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }
    }
}
