using AutoMapper;
using StudentAdminPortal.API.DTO;
using StudentAdminPortal.API.Models;
using System;
using MainModel = StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.StudentAdminPortalMapper.AfterMap
{
    // <Source, Destination>
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentDto, MainModel.Student>
    {
        public void Process(AddStudentDto source, Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new MainModel.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
