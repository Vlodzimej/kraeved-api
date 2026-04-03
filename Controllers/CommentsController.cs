using KraevedAPI.Models;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KraevedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IKraevedService _kraevedService;
        public CommentsController(IKraevedService kraevedService)
        {
            _kraevedService = kraevedService;
        }

        [HttpGet("geo-object/{geoObjectId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByGeoObjectId(int geoObjectId)
        {
            var result = await _kraevedService.GetCommentsByGeoObjectId(geoObjectId);
            return Ok(result);
        }

        [HttpGet("geo-object/{geoObjectId}/latest")]
        [AllowAnonymous]
        public async Task<ActionResult<Comment?>> GetLatestCommentByGeoObjectId(int geoObjectId)
        {
            var result = await _kraevedService.GetLatestCommentByGeoObjectId(geoObjectId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddComment([FromBody] CommentRequest request)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var result = await _kraevedService.AddComment(request.GeoObjectId, userId, request.Text);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var result = await _kraevedService.DeleteComment(id, userId);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }

    public class CommentRequest
    {
        public int GeoObjectId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
