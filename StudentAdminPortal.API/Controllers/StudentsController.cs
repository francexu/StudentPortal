using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DTO;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Repository;
using StudentAdminPortal.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository repository, IMapper mapper, IImageRepository imageRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _repository.GetStudentsAsync();
            return Ok(_mapper.Map<List<StudentDto>>(students));
        }

        [HttpGet]
        [Route("{studentId:guid}"), ActionName("GetStudent")] 
        public async Task<IActionResult> GetStudent([FromRoute] Guid studentId)
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

        [HttpDelete]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await _repository.StudentExists(studentId))
            {
                var student = await _repository.DeleteStudentAsync(studentId);

                return Ok(_mapper.Map<StudentDto>(student));
            }

            return NotFound();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentDto addStudentDto)
        {
            var newStudent = await _repository.AddStudentAsync(_mapper.Map<Student>(addStudentDto));

            // best practice to use 201 (createdataction) for post requests
            // yung last parameter dapat yung object na pinass mo dito sa method pero since naka Student siya, kailangan pa i-convert sa dto
            return CreatedAtAction(nameof(GetStudent), new { studentId = newStudent.Id}, _mapper.Map<StudentDto>(newStudent));
        }

        [HttpPost("{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            // Check if student exists
            if (await _repository.StudentExists(studentId))
            {
                // create a new file name for the image
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                // Upload the Image to local storage
                var fileImagePath = await _imageRepository.Upload(profileImage, fileName);

                // Update the profile image path in the database
                if (await _repository.UpdateProfileImage(studentId, fileImagePath))
                {
                    return Ok(fileImagePath);
                };

                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
            }

            // Otherwise
            return NotFound();
        }
    }
}
