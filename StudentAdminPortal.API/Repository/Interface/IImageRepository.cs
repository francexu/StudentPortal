using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repository.Interface
{
    public interface IImageRepository
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}
