using StudentAdminPortal.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(Guid studentId); 

        Task<List<Gender>> GetGendersAsync();

        Task<bool> StudentExists(Guid studentId);

        Task<Student> UpdateStudent(Guid studentId, Student student);
    }
}
