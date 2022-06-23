using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace PlaygroundAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        IActionResult Upload()
        {
            try
            {
                // extract the file from the request
                var file = Request.Form.Files[0];

                var folderName = "Resources";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    file.CopyTo(stream);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}