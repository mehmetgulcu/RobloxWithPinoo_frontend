namespace RobloxWithPinoo_UI.Services.CloudinaryService
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile image);
        Task<List<string>> ListImagesAsync();
    }
}
