using AutoMapper;
using StudentAdminPortal.API.DTO;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.StudentAdminPortalMapper.AfterMap;

namespace StudentAdminPortal.API.Mapper
{
    public class StudentAdminPortalMappings : Profile
    {
        public StudentAdminPortalMappings()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            // after map is to map yung properties na wala directly sa main model
            CreateMap<UpdateStudentDto, Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();
            CreateMap<AddStudentDto, Student>()
                .AfterMap<AddStudentRequestAfterMap>();
        }
    }
}
