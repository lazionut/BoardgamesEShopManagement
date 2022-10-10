using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Models
{
    public class UploadFileRequest
    {
        public string FilePath { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}
