using Microsoft.AspNetCore.Http;
using StudentAdminPortal.API.Repository.Interface;
using System.IO;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repository
{
    public class ImageRepository : IImageRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            // Create File Path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);

            // Use FileStream - used for reading and writing files
            // FileMode.Create creates a new file if it doesn't exist or overwrites one if it does
            // babasahin ni FileStream yung na-create na file sa filePath
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            // kokopyahin yung bytes ni uploaded image file papunta sa fileStream
            await file.CopyToAsync(fileStream);

            return GetRelativePath(fileName);
        }

        private string GetRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}
