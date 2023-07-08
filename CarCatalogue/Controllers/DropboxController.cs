using CarCatalogue.Common.Constants;

using Dropbox.Api;
using Dropbox.Api.Files;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogue.Controllers
{
    [Route("[controller]")]
    public class DropboxController : Controller
    {
        private readonly IConfiguration _configuration;

        public DropboxController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("upload")]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> Upload(string folderName, string fileName, IFormFile content)
        {
            var accessToken = _configuration.GetValue<string>("DropBox:AccessToken");

            using var dbx = new DropboxClient(accessToken);

            var account = await dbx.Users.GetCurrentAccountAsync();

            var path = $"/{folderName}/{fileName}";

            var metadata = await dbx.Files.UploadAsync(
                path,
                WriteMode.Overwrite.Instance,
                body: content.OpenReadStream());

            // Creating a sharedlink, because DropBox doesn't support permanent links and this seems to be the workaround.
            var link = (await dbx.Sharing.CreateSharedLinkWithSettingsAsync(path)).Url;

            // Removing the dl=0 parameter and adding raw=1 to make the link lead to the raw image, instead of dropbox website.
            var imageLink = link.TrimEnd(new char[] { 'd', 'l', '=', '0' }) + "raw=1";

            return Ok(imageLink);
        }
    }
}