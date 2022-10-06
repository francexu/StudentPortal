using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DTO;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Repository;
using StudentAdminPortal.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _repository.GetStudentsAsync();
            return Ok(_mapper.Map<List<StudentDto>>(students));
        }

        [HttpGet]
        [Route("{studentId:guid}")] 
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            // Fetch student detail
            var student = await _repository.GetStudentAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudentDto>(student));
        }

        [HttpPut]
        // WALANG SPACE MAG-EERROR PAG MERON
        [Route("{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentDto updateStudentDto)
        {
            if (await _repository.StudentExists(studentId))
            {
                // Update Details
                // si dto icoconvert mo into kay student para malagay siya sa db
                var updatedStudent = await _repository.UpdateStudent(studentId, _mapper.Map<Student>(updateStudentDto));

                if (updateStudentDto != null)
                {
                    // convert the main model to a dto para di maexpose
                    return Ok(_mapper.Map<Student>(updatedStudent));
                }
            } 

            return NotFound();
        }
    }
}
