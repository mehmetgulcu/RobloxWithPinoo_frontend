
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace RobloxWithPinoo_UI.Services.CloudinaryService
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<List<string>> ListImagesAsync()
        {
            var listParams = new ListResourcesParams();
            var result = await _cloudinary.ListResourcesAsync(listParams);

            return result.Resources.Select(r => r.Url.ToString()).ToList();
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream())
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }
    }
}
