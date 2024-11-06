using HastaneYonetimSistemiApp.Business.Operations.Appointment;
using HastaneYonetimSistemiApp.Business.Operations.Doctor;
using HastaneYonetimSistemiApp.Business.Operations.Doctor.Dto;
using HastaneYonetimSistemiApp.Business.Operations.Patient;
using HastaneYonetimSistemiApp.Business.Operations.Patient.Dto;
using HastaneYonetimSistemiApp.Business.Operations.User.Dtos;
using HastaneYonetimSistemiApp.Data.Enums;
using HastaneYonetimSistemiApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HastaneYonetimSistemiApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff,Doctor")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = await _doctorService.GetDoctor(id);
            if (doctor is null) 
            return NotFound();
            else
                return Ok(doctor);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Staff,Doctor")]
        public async Task<IActionResult>GetDoctors()
        {
            var doctors = await _doctorService.GetAllDoctors();
            return Ok(doctors);
        }

        [HttpPost]
       // [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddDoctor(AddDoctorRequest request)
        {
            var addDoctorDto = new AddDoctorDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DoctorSpeciality = request.DoctorSpeciality,
                PhoneNumber = request.PhoneNumber,
                PoliclinicId = request.PoliclinicId,
            };

            var result = await _doctorService.AddDoctor(addDoctorDto);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("FirstName")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditDoctorFirstName(int id, string changeTo)
        {
            var result = await _doctorService.EditDoctorFirstName(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("LastName")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditDoctorLastName(int id, string changeTo)
        {
            var result = await _doctorService.EditDoctorLastName(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("DoctorSpeciality")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditDoctorSpeciality(int id, DoctorSpeciality changeTo)
        {
            var result = await _doctorService.EditDoctorSpeciality(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("PhoneNumber")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditPhoneNumber(int id, string changeTo)
        {
            var result = await _doctorService.EditPhoneNumber(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("Policlinic")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditPoliclinicId(int id, int changeTo)
        {
            var result = await _doctorService.EditPoliclinicId(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _doctorService.DeleteDoctor(id);

            if (result.IsSucced)
                return Ok();
            return
                BadRequest(result.Massage);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> UpdateDoctor(int id, UpdateDoctorRequest request)
        {
            var updateDoctorDto = new UpdateDoctorDto
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DoctorSpeciality = request.DoctorSpeciality,
                PhoneNumber = request.PhoneNumber,
                PoliclinicId = request.PoliclinicId,

            };

            var result = await _doctorService.UpdateDoctor(updateDoctorDto);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
    }
}
