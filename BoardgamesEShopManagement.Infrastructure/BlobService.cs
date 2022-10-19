using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Extensions;
using BlobInfo = BoardgamesEShopManagement.Domain.Models.BlobInfo;

namespace BoardgamesEShopManagement.Infrastructure
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<bool> UploadFileBlobAsync(string filePath, string fileName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("blobimages");
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            try
            {
                await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = filePath.GetContentType() });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BlobInfo?> GetBlobAsync(string name)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("blobimages");
            BlobClient blobClient = containerClient.GetBlobClient(name);

            try
            {
                var blobDownloadInfo = await blobClient.DownloadAsync();
                return new BlobInfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteBlobAsync(string blobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("blobimages");
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            return await blobClient.DeleteIfExistsAsync();
        }
    }
}
