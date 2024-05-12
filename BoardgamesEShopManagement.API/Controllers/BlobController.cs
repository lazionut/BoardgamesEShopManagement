using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardgamesEShopManagement.API.Controllers
{
    [Route("api/blobs")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BlobController : Controller
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] FileModel file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.FormFile.CopyTo(stream);
                }

                bool isFileUploaded = await _blobService.UploadFileBlobAsync(path, file.FileName);

                if (!isFileUploaded)
                {
                    return NotFound();
                }

                try
                {
                    System.IO.File.Delete(path);
                }
                catch
                {
                    return NotFound();
                }
            }
            catch
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("{blobName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFile(string blobName)
        {
            BlobInfo? data = await _blobService.GetBlobAsync(blobName);

            if (data == null)
            {
                return NotFound();
            }

            return File(data.Content, data.ContentType);
        }

        [HttpDelete("{blobName}")]
        public async Task<IActionResult> DeleteFile(string blobName)
        {
            bool isBlobDeleted = await _blobService.DeleteBlobAsync(blobName);

            if (!isBlobDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}