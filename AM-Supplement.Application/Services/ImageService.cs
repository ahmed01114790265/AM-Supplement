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

    public class ImageService : IImageService
    {
        private readonly string _storagePath;
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public ImageService(IConfiguration config)
        {
            // قراءة المسار من appsettings.json
            // إذا لم يجد القيمة، سيستخدم مجلد افتراضي خارج مجلدات المشروع
            _storagePath = config["StorageSettings:StaticFolder"] ?? Path.Combine(Directory.GetCurrentDirectory(), "SharedStorage");
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) return "default-product.png";

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_permittedExtensions.Contains(extension))
                throw new InvalidOperationException("Invalid file type.");

            // إنشاء المسار: BasePath/folderName
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
            // لا تحذف الصورة الافتراضية أبداً
            if (string.IsNullOrEmpty(fileName) || fileName == "default-product.png") return;

            string fullPath = Path.Combine(_storagePath, folderName, fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}

