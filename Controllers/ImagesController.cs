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
            var thumbPath = Path.Combine(rootFolder, "thumbnails", filename);

            if (System.IO.File.Exists(thumbPath)) {
                new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(thumbPath), out var contentType);
                var fileContents = await System.IO.File.ReadAllBytesAsync(thumbPath);
                return File(fileContents, contentType ?? "application/octet-stream", Path.GetFileName(thumbPath));
            }

            var originalPath = Path.Combine(rootFolder, "images", filename);
            if (!System.IO.File.Exists(originalPath)) {
                return NotFound();
            }

            new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(originalPath), out var contentType2);
            var originalContents = await System.IO.File.ReadAllBytesAsync(originalPath);
            return File(originalContents, contentType2 ?? "application/octet-stream", Path.GetFileName(originalPath));
        }

        [AllowAnonymous]
        [HttpGet("preview/{filename}")]
        public async Task<ActionResult<String>?> DownloadPreview(string filename) {
            var rootFolder = Directory.GetCurrentDirectory();
            var previewPath = Path.Combine(rootFolder, "previews", filename);

            if (System.IO.File.Exists(previewPath)) {
                new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(previewPath), out var contentType);
                var fileContents = await System.IO.File.ReadAllBytesAsync(previewPath);
                return File(fileContents, contentType ?? "application/octet-stream", Path.GetFileName(previewPath));
            }

            var originalPath = Path.Combine(rootFolder, "images", filename);
            if (!System.IO.File.Exists(originalPath)) {
                return NotFound();
            }

            new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(originalPath), out var contentType2);
            var originalContents = await System.IO.File.ReadAllBytesAsync(originalPath);
            return File(originalContents, contentType2 ?? "application/octet-stream", Path.GetFileName(originalPath));
        }

        [AllowAnonymous]
        [HttpGet("avatar/{filename}")]
        public async Task<ActionResult<String>?> DownloadAvatar(string filename) {
            var rootFolder = Directory.GetCurrentDirectory();
            var path = Path.Combine(rootFolder, "avatars");

            var filepath = Path.Combine(path, filename);
            if (!System.IO.File.Exists(filepath)) {
                return NotFound();
            }

            new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(filepath), out var contentType);
            var fileContents = await System.IO.File.ReadAllBytesAsync(filepath);

            return File(fileContents, contentType ?? "application/octet-stream", Path.GetFileName(filepath));
        }

        [HttpDelete("{filename}")]
        public async Task<ActionResult> DeleteImage(string filename) {
            try {
                await _kraevedService.DeleteImage(filename);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
