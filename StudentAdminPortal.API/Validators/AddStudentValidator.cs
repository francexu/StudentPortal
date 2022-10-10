using FluentValidation;
using StudentAdminPortal.API.DTO;
using StudentAdminPortal.API.Repository.Interface;
using System.Linq;

namespace StudentAdminPortal.API.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentDto>
    {
        public AddStudentValidator(IStudentRepository studentRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).LessThanOrEqualTo(09999999999).GreaterThan(09000000000);
            // the id should exist in the database
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList().FirstOrDefault(x => x.Id == id);

                if (gender != null)
                {
                    return true;
                }

                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();
        }
    }
}
