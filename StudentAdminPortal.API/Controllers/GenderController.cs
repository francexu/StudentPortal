using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DTO;
using StudentAdminPortal.API.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace StudentAdminPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GenderController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenders()
        {
            var genderList =  await _studentRepository.GetGendersAsync();

            if(genderList == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<GenderDto>>(genderList));
        }
    }
}
