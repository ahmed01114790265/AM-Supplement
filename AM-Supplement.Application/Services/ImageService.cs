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
    using Azure.Storage.Blobs;

    public class ImageService : IImageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "products-images"; // تأكد إن ده نفس الاسم في Azure
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public ImageService(IConfiguration config)
        {
            // بيقرأ الـ Connection String من الملف اللي إنت لسه مجهزه
            var connectionString = config["AzureBlobStorage"]?.Trim();

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("AzureBlobStorage connection string is missing!");
            }

            try
            {
                _blobServiceClient = new BlobServiceClient(connectionString);
            }
            catch (FormatException ex)
            {
                // ده هيخلينا نعرف لو المشكلة لسه في النص نفسه
                throw new FormatException("The Azure Storage connection string has an invalid format. Please check Azure Portal.", ex);
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folderName)
        {
            // في حالة مفيش صورة، رجع رابط لصورة افتراضية (يفضل ترفع واحدة مسبقاً)
            if (file == null || file.Length == 0)
                return "https://imagesforalldb.blob.core.windows.net/products-images/default-product.png";

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_permittedExtensions.Contains(extension))
                throw new InvalidOperationException("Invalid file type.");

            // الوصول للـ Container
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // التأكد إن الـ Container موجود
            await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            // إنشاء اسم فريد للملف مع الحفاظ على الفولدر (folderName)
            // في Blob Storage الفولدرات بتبقى مجرد جزء من اسم الملف
            string fileName = $"{folderName}/{Guid.NewGuid()}{extension}";
            var blobClient = containerClient.GetBlobClient(fileName);

            // الرفع للسحابة
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new Azure.Storage.Blobs.Models.BlobHttpHeaders { ContentType = file.ContentType });
            }

            // أهم جزء: بنرجع الرابط الكامل اللي هيتخزن في الداتابيز
            return blobClient.Uri.ToString();
        }

        public async Task DeleteImage(string fileUri, string folderName)
        {
            // لو الرابط فاضي أو صورة افتراضية مش هنمسح حاجة
            if (string.IsNullOrEmpty(fileUri) || fileUri.Contains("default-product.png")) return;

            try
            {
                // استخراج اسم الـ Blob من الـ URI
                var uri = new Uri(fileUri);
                // السطر ده بيجيب كل حاجة بعد اسم الـ Container (بما فيها الفولدر واسم الملف)
                string blobName = string.Join("", uri.Segments.Skip(2));

                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                var blobClient = containerClient.GetBlobClient(Uri.UnescapeDataString(blobName));

                await blobClient.DeleteIfExistsAsync();
            }
            catch
            {
                // فشل المسح مش لازم يوقف الأبلكيشن، بس ممكن تعمل Log هنا
            }
        }
    }
}

