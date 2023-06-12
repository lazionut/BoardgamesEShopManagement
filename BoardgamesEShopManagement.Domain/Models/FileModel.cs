using Microsoft.AspNetCore.Http;

namespace BoardgamesEShopManagement.Domain.Models
{
    public class FileModel
    {
        public string FileName { get; set; } = null!;
        public IFormFile FormFile { get; set; } = null!;
    }
}