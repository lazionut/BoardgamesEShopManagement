using BoardgamesEShopManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Abstract
{
    public interface IBlobService
    {
        public Task<bool> UploadFileBlobAsync(string filePath, string fileName);
        public Task<BlobInfo?> GetBlobAsync(string name);
        Task<bool> DeleteBlobAsync(string blobName);
    }
}
