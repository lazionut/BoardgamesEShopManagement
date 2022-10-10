using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Models;
using Bogus.DataSets;
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
        public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        {
            bool isFileUploaded = await _blobService.UploadFileBlobAsync(request.FilePath, request.FileName);

            if (isFileUploaded == false)
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

            if (isBlobDeleted == false)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
