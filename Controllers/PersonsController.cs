using KraevedAPI.Models;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KraevedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonsController : ControllerBase
    {
        private readonly IKraevedService _kraevedService;
        public PersonsController(IKraevedService kraevedService)
        {
            _kraevedService = kraevedService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPersons()
        {
            var result = await _kraevedService.GetAllPersons();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Person?>> GetPersonById(int id)
        {
            var result = await _kraevedService.GetPersonById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> InsertPerson(Person person)
        {
            var result = await _kraevedService.InsertPerson(person);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePerson(Person person)
        {
            var result = await _kraevedService.UpdatePerson(person);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var result = await _kraevedService.DeletePerson(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("geo-object/{geoObjectId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonsByGeoObjectId(int geoObjectId)
        {
            var result = await _kraevedService.GetPersonsByGeoObjectId(geoObjectId);
            return Ok(result);
        }

        [HttpGet("{personId}/geo-objects")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GeoObject>>> GetGeoObjectsByPersonId(int personId)
        {
            var result = await _kraevedService.GetGeoObjectsByPersonId(personId);
            return Ok(result);
        }

        [HttpPost("link")]
        public async Task<ActionResult> LinkPersonToGeoObject([FromBody] LinkRequest request)
        {
            var result = await _kraevedService.LinkPersonToGeoObject(request.PersonId, request.GeoObjectId);
            return Ok(result);
        }

        [HttpDelete("link")]
        public async Task<ActionResult> UnlinkPersonFromGeoObject([FromBody] LinkRequest request)
        {
            var result = await _kraevedService.UnlinkPersonFromGeoObject(request.PersonId, request.GeoObjectId);
            return Ok(result);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Person>>> SearchPersons([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 3) return Ok(Array.Empty<Person>());
            var result = await _kraevedService.SearchPersons(q);
            return Ok(result);
        }

        [HttpGet("relation-types")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PersonRelationType>>> GetAllRelationTypes()
        {
            var result = await _kraevedService.GetAllRelationTypes();
            return Ok(result);
        }

        [HttpGet("{id}/relations")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PersonRelationDto>>> GetRelationsByPersonId(int id)
        {
            var result = await _kraevedService.GetRelationsByPersonId(id);
            return Ok(result);
        }

        [HttpGet("{id}/tree")]
        [AllowAnonymous]
        public async Task<ActionResult<PersonTreeNode>> GetFamilyTree(int id)
        {
            var result = await _kraevedService.GetFamilyTree(id);
            return Ok(result);
        }

        [HttpPost("relation")]
        public async Task<ActionResult> AddRelation([FromBody] RelationRequest request)
        {
            var result = await _kraevedService.AddRelation(request.PersonId1, request.PersonId2, request.RelationTypeId);
            return Ok(result);
        }

        [HttpDelete("relation")]
        public async Task<ActionResult> RemoveRelation([FromBody] RelationRequest request)
        {
            var result = await _kraevedService.RemoveRelation(request.PersonId1, request.PersonId2, request.RelationTypeId);
            return Ok(result);
        }
    }

    public class RelationRequest
    {
        public int PersonId1 { get; set; }
        public int PersonId2 { get; set; }
        public int RelationTypeId { get; set; }
    }

    public class LinkRequest
    {
        public int PersonId { get; set; }
        public int GeoObjectId { get; set; }
    }
}
