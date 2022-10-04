using AutoMapper;
using StudentAdminPortal.API.DTO;
using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Mapper
{
    public class StudentAdminPortalMappings : Profile
    {
        public StudentAdminPortalMappings()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
