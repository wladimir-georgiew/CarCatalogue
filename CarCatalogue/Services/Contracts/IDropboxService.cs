namespace CarCatalogue.Services.Contracts
{
    public interface IDropboxService
    {
        Task<string> UploadAsync(string folderName, string fileName, IFormFile content);
    }
}
