using KraevedAPI.Constants;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace KraevedAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IKraevedService _kraevedService;
        public ImagesController(IKraevedService kraevedService)
        {
            _kraevedService = kraevedService;
        }

        [HttpPost]
        public async Task<ActionResult<String>?> UploadImage(IEnumerable<IFormFile> imageFiles) {
            List<string> filenames;

            try {
                filenames = await _kraevedService.UploadImages(imageFiles);
            }

            catch(Exception ex) {
                return BadRequest(new { ex.Message });
            }

            return Ok(new { filenames });
        }

        [AllowAnonymous]
        [HttpGet("filename/{filename}")]
        public async Task<ActionResult<String>?> DownloadImage(string filename) {
            var rootFolder = Directory.GetCurrentDirectory();
            var path = Path.Combine(rootFolder, "images");

            var filepath = Path.Combine(path, filename); 
            new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(filepath), out var contentType);
            var fileContents = await System.IO.File.ReadAllBytesAsync(filepath);

            return File(fileContents, contentType ?? "application/octet-stream", Path.GetFileName(filepath));
        }

        [AllowAnonymous]
        [HttpGet("thumbnail/{filename}")]
        public async Task<ActionResult<String>?> DownloadThumbnail(string filename) {
            var rootFolder = Directory.GetCurrentDirectory();
            var path = Path.Combine(rootFolder, "thumbnails");

            var filepath = Path.Combine(path, filename);
            if (!System.IO.File.Exists(filepath)) {
                return NotFound();
            }

            new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(filepath), out var contentType);
            var fileContents = await System.IO.File.ReadAllBytesAsync(filepath);

            return File(fileContents, contentType ?? "application/octet-stream", Path.GetFileName(filepath));
        }

        [AllowAnonymous]
        [HttpGet("preview/{filename}")]
        public async Task<ActionResult<String>?> DownloadPreview(string filename) {
            var rootFolder = Directory.GetCurrentDirectory();
            var path = Path.Combine(rootFolder, "previews");

            var filepath = Path.Combine(path, filename);
            if (!System.IO.File.Exists(filepath)) {
                return NotFound();
            }

            new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(filepath), out var contentType);
            var fileContents = await System.IO.File.ReadAllBytesAsync(filepath);

            return File(fileContents, contentType ?? "application/octet-stream", Path.GetFileName(filepath));
        }
    }
}
