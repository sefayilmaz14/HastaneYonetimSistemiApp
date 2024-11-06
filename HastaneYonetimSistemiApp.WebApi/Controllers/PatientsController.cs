using HastaneYonetimSistemiApp.Business.Operations.Doctor;
using HastaneYonetimSistemiApp.Business.Operations.Patient;
using HastaneYonetimSistemiApp.Business.Operations.Patient.Dto;
using HastaneYonetimSistemiApp.Data.Enums;
using HastaneYonetimSistemiApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HastaneYonetimSistemiApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _patientService.GetPatient(id);
            if (patient is null)
                return NotFound();
            else
                return Ok(patient);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _patientService.GetAllPatients();
            return Ok(patients);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> AddPatient(AddPatientRequest request)
        {
            var addPatientDto = new AddPatientDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
            };

            var result = await _patientService.AddPatient(addPatientDto);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);

        }
        [HttpPatch("FirstName")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditPatientFirstName(int id, string changeTo)
        {
            var result = await _patientService.EditPatientFirstName(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("LastName")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditPatientLastName(int id, string changeTo)
        {
            var result = await _patientService.EditPatientLastName(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("BirthDate")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditBirthDate(int id, DateTime changeTo)
        {
            var result = await _patientService.EditBirthDate(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("Gender")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditGender(int id, Gender changeTo)
        {
            var result = await _patientService.EditGender(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientService.DeletePatient(id);
            
            if (result.IsSucced)
                return Ok();
            return 
                BadRequest(result.Massage);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> UpdatePatient(int id,UpdatePatientRequest request)
        {
            var updatePatientDto = new UpdatePatientDto
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Gender = request.Gender,

            };

            var result = await _patientService.UpdatePatient(updatePatientDto);
            
            if(result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
    }
}
