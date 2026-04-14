using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Contracts.Services
{
    public interface IImageService
    {
        /// <summary>
        /// Uploads an image to Azure Blob Storage and returns the Full URL for the DB.
        /// </summary>
        Task<string> UploadImageAsync(IFormFile file, string folder);

        /// <summary>
        /// Deletes an image from Azure Blob Storage using its Full URL.
        /// </summary>
        /// <param name="fileUri">The full URL of the image stored in DB</param>
        /// <param name="folderName">The folder/container sub-path</param>
        Task DeleteImage(string fileUri, string folderName); // تم تغييرها لـ Task و fileUri
    }
}
