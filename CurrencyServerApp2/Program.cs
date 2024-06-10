
namespace FileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public FileController()
        {
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFiles(IFormFileCollection files)
        {
            if (!(files != null && !files) = 0)
            {
                return BadRequest("No files uploaded.");
            }

            foreach (var file in files)
            {
                var filePath = Path.Combine(_storagePath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok("Files uploaded successfully.");
        }

        private IActionResult BadRequest(string v)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            var filePath = Path.Combine(_storagePath, fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        private IActionResult File(byte[] fileBytes, string v, string fileName)
        {
            throw new NotImplementedException();
        }

        private IActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{fileName}")]
        public IActionResult DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_storagePath, fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            System.IO.File.Delete(filePath);
            return Ok("File deleted successfully.");
        }

        private IActionResult Ok(string v)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("delete-multiple")]
        public IActionResult DeleteFiles([FromBody] string[] fileNames)
        {
            foreach (var fileName in fileNames)
            {
                var filePath = Path.Combine(_storagePath, fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            return Ok("Files deleted successfully.");
        }
    }

    internal class FromBodyAttribute : Attribute
    {
    }

    internal class HttpDeleteAttribute : Attribute
    {
        private string v; public HttpDeleteAttribute(string v)
        {
            this.v = v;
        }
    }

    internal class HttpGetAttribute : Attribute
    {
        private string v; public HttpGetAttribute(string v)
        {
            this.v = v;
        }
    }

    public interface IFormFileCollection
    {
    }

    public interface IActionResult
    {
    }

    internal class HttpPostAttribute : Attribute
    {
        private string v;

        public HttpPostAttribute(string v)
        {
            this.v = v;
        }
    }

    public class ControllerBase
    {
    }

    internal class ApiControllerAttribute : Attribute
    {
    }

    internal class RouteAttribute : Attribute
    {
        private string v;

        public RouteAttribute(string v)
        {
            this.v = v;
        }
    }
}
