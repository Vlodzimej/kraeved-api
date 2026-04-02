using KraevedAPI.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        private const int ThumbnailMaxSize = 200;
        private const int PreviewMaxSize = 800;
        private const string ThumbnailsFolder = "thumbnails";
        private const string PreviewsFolder = "previews";

        /// <summary>
        /// Загрузка изображения на сервер с созданием уменьшенных версий
        /// </summary>
        /// <param name="imageFiles">Коллекция файлов с изображениями</param>
        /// <returns></returns>
        public async Task<List<string>> UploadImages(IEnumerable<IFormFile> imageFiles)
        {
            var result = new List<string>();

            IEnumerable<IFormFile> filteredImageFiles = imageFiles.Where(item => item.Length > 0) ?? [];

            if (!filteredImageFiles.Any()) {
                throw new Exception(ServiceConstants.Exception.FileIsEmpty);
            }

            var rootFolder = Directory.GetCurrentDirectory();
            var imagesPath = Path.Combine(rootFolder, "images");
            var thumbnailsPath = Path.Combine(rootFolder, ThumbnailsFolder);
            var previewsPath = Path.Combine(rootFolder, PreviewsFolder);

            if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);
            if (!Directory.Exists(thumbnailsPath)) Directory.CreateDirectory(thumbnailsPath);
            if (!Directory.Exists(previewsPath)) Directory.CreateDirectory(previewsPath);

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 20
            };

            var lockObj = new object();

            await Parallel.ForEachAsync(filteredImageFiles, options, async (imageFile, token) =>
            {
                if (imageFile.Length == 0) {
                    throw new Exception(ServiceConstants.Exception.FileIsEmpty);
                }

                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
                if (!allowedExtension.Contains(ext)) {
                    throw new Exception(ServiceConstants.Exception.WrongExtension);
                }

                var uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;

                // Save original
                var originalPath = Path.Combine(imagesPath, newFileName);
                await using (var fileStream = new FileStream(originalPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream, token);
                }

                // Create thumbnail and preview
                using var image = await Image.LoadAsync(originalPath, token);
                image.Mutate(x => x.AutoOrient());

                // Thumbnail
                var thumbnailFileName = uniqueString + GetThumbExtension(ext);
                var thumbnailPath = Path.Combine(thumbnailsPath, thumbnailFileName);
                using var thumbnail = image.Clone(ctx =>
                    ctx.Resize(new ResizeOptions
                    {
                        Size = new Size(ThumbnailMaxSize, ThumbnailMaxSize),
                        Mode = ResizeMode.Max,
                    })
                );
                await SaveImageAsync(thumbnail, thumbnailPath, ext, token);

                // Preview
                var previewFileName = uniqueString + GetThumbExtension(ext);
                var previewPath = Path.Combine(previewsPath, previewFileName);
                using var preview = image.Clone(ctx =>
                    ctx.Resize(new ResizeOptions
                    {
                        Size = new Size(PreviewMaxSize, PreviewMaxSize),
                        Mode = ResizeMode.Max,
                    })
                );
                await SaveImageAsync(preview, previewPath, ext, token);

                lock (lockObj)
                {
                    result.Add(newFileName);
                }
            });

            return result;
        }

        private static string GetThumbExtension(string originalExt)
        {
            return originalExt.Equals(".png", StringComparison.OrdinalIgnoreCase) ? ".png" : ".jpg";
        }

        private static async Task SaveImageAsync(Image image, string path, string originalExt, CancellationToken token)
        {
            if (originalExt.Equals(".png", StringComparison.OrdinalIgnoreCase))
            {
                await image.SaveAsPngAsync(path, token);
            }
            else
            {
                await image.SaveAsJpegAsync(path, new JpegEncoder { Quality = 85 }, token);
            }
        }
    }
}
