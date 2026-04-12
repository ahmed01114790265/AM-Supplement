using AM_Supplement.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Application.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting;

    public class ImageService : IImageService
    {
        private readonly string _storagePath;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public ImageService(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

            // نقرأ القيمة من الإعدادات
            var configPath = config["StorageSettings:StaticFolder"];

            // إذا كانت القيمة مسار كامل (يحتوي على :) أو فارغة، نستخدم مسار الـ wwwroot الافتراضي في Azure
            if (string.IsNullOrEmpty(configPath) || configPath.Contains(":"))
            {
                // هذا هو المسار الآمن في Azure: D:\home\site\wwwroot\images
                _storagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            }
            else
            {
                // إذا كانت القيمة مجرد اسم مجلد مثل "images"، ندمجها مع مسار الويب
                _storagePath = Path.Combine(_webHostEnvironment.WebRootPath, configPath);
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) return "default-product.png";

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_permittedExtensions.Contains(extension))
                throw new InvalidOperationException("Invalid file type.");

            // تأمين إنشاء المجلدات داخل wwwroot
            string targetFolder = Path.Combine(_storagePath, folderName);

            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            string fileName = $"{Guid.NewGuid()}{extension}";
            string physicalPath = Path.Combine(targetFolder, fileName);

            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public void DeleteImage(string fileName, string folderName)
        {
            if (string.IsNullOrEmpty(fileName) || fileName == "default-product.png") return;

            string fullPath = Path.Combine(_storagePath, folderName, fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}

