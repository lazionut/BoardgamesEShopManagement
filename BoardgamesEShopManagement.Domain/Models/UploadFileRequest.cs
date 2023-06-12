namespace BoardgamesEShopManagement.Domain.Models
{
    public class UploadFileRequest
    {
        public string FilePath { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}