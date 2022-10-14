using BoardgamesEShopManagement.Domain.Models;

namespace BoardgamesEShopManagement.Application.Abstract
{
    public interface IBlobService
    {
        public Task<bool> UploadFileBlobAsync(string filePath, string fileName);
        public Task<BlobInfo?> GetBlobAsync(string name);
        Task<bool> DeleteBlobAsync(string blobName);
    }
}
