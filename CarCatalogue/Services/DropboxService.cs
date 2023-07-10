using Dropbox.Api.Files;
using Dropbox.Api;

using CarCatalogue.Services.Contracts;

namespace CarCatalogue.Services
{
    public class DropboxService : IDropboxService
    {
        private readonly IConfiguration _configuration;

        public DropboxService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadAsync(string folderName, string fileName, IFormFile content)
        {
            var accessToken = _configuration.GetValue<string>("DropBox:AccessToken");

            using var dbx = new DropboxClient(accessToken);

            var path = $"/{folderName}/{fileName}";

            var metadata = await dbx.Files.UploadAsync(
                path,
                WriteMode.Overwrite.Instance,
                body: content.OpenReadStream());

            var link = string.Empty;

            // First check if the image the user is trying to upload is already uploaded to the exact car
            var sharedLinks = await dbx.Sharing.ListSharedLinksAsync(path);
            if (sharedLinks.Links.Where(x => x.IsFile).Any())
            {
                link = sharedLinks.Links.First().Url;
            }
            else
            {
                // Creating a sharedlink, because DropBox doesn't support permanent links and this seems to be the workaround.
                link = (await dbx.Sharing.CreateSharedLinkWithSettingsAsync(path)).Url;
            }

            // Removing the dl=0 parameter and adding raw=1 to make the link lead to the raw image, instead of dropbox website.
            var imageLink = link.TrimEnd(new char[] { 'd', 'l', '=', '0' }) + "raw=1";

            return imageLink;
        }
    }
}
