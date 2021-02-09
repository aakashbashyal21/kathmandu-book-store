using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IImageUpload
    {
      string RemoveOldImage(string filePath, string rootPath);
        Task<string> UploadImage(string folderPath, IFormFile file, string rootPath);
    }
}
