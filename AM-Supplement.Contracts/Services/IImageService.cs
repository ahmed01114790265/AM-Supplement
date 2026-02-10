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
        /// Saves an IFormFile to the server and returns the web-relative path for the DB.
        /// </summary>
        Task<string> UploadImageAsync(IFormFile file, string folder);

           /// <summary>
           /// Deletes an image from the physical storage.
           /// </summary>
           void DeleteImage(string fileName, string folderName);
        }
   
}
