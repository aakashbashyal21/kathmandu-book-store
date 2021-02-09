using BookStore.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Service
{
    public class ImageUploadService : IImageUpload
    {
        public string RemoveOldImage(string filePath, string rootPath)
        {
            var fullPath = rootPath+ "/"  + filePath;

           // string newPathForFile = Path.Combine(rootPath, "\\books\\cover\\",Path.GetFileName(filePath));


            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            string ok = "ok";
            return ok;
        }

        public async Task<string> UploadImage(string folderPath, IFormFile file, string rootPath)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(rootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }
    }
}
