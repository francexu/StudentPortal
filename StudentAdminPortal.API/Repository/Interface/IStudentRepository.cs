using StudentAdminPortal.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
    }
}
