using AutoMapper;
using StudentAdminPortal.API.DTO;
using MainModel = StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.StudentAdminPortalMapper.AfterMap
{
    // this class is to map the physical address and postal address since hindi siya mahahanap ni mapper kasi under siya ng Gender and wala siya directly sa main model
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentDto, MainModel.Student>
    {
        public void Process(UpdateStudentDto source, MainModel.Student destination, ResolutionContext context)
        {
            destination.Address = new MainModel.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
