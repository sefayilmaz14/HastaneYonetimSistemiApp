using HastaneYonetimSistemiApp.Business.Operations.Appointment;
using HastaneYonetimSistemiApp.Business.Operations.Appointment.Dto;
using HastaneYonetimSistemiApp.Business.Operations.Doctor;
using HastaneYonetimSistemiApp.Business.Operations.Patient.Dto;
using HastaneYonetimSistemiApp.Business.Operations.Patient;
using HastaneYonetimSistemiApp.Data.Enums;
using HastaneYonetimSistemiApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HastaneYonetimSistemiApp.WebApi.Filters;

namespace HastaneYonetimSistemiApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;


        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("patient")]
        
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _appointmentService.GetPatientAppointment(id);
            if (patient is null)
                return NotFound();
            else
                return Ok(patient);
        }

        [HttpGet("doctor")]
        [Authorize(Roles = "Admin,Staff,Doctor")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctor = await _appointmentService.GetDoctorAppointment(id);
            if (doctor is null)
                return NotFound();
            else
                return Ok(doctor);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff,Doctor")]
        public async Task<IActionResult> Get(int id)
        {
            var appointment = await _appointmentService.GetAppointment(id);
            if (appointment is null)
                return NotFound();
            else
                return Ok(appointment);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Staff,Doctor")]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            return Ok(appointments);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Patient")]
       // [TimeControlFilter]
        public async Task<IActionResult> AddAppointment(AddAppointmentRequest request)
        {
            var addAppointmentDto = new AddAppointmentDto
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                AppointmentDate = request.AppointmentDate,
                AppointmentStatus = request.AppointmentStatus,
                AppointmentType = request.AppointmentType,
                RoomNumber = request.RoomNumber,
            };

            var result = await _appointmentService.AddAppointment(addAppointmentDto);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);


        }
        [HttpPatch("DoctorId")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditAppointmentDoctorId(int id, int changeTo)
        {
            var result = await _appointmentService.EditAppointmentDoctorId(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("PatientId")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditAppointmentPatientId(int id, int changeTo)
        {
            var result = await _appointmentService.EditAppointmentPatientId(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("Appointment Date")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditAppointmentDate(int id, DateTime changeTo)
        {
            var result = await _appointmentService.EditAppointmentDate(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("Appointment Status")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditAppointmentStatus(int id, AppointmentStatus changeTo)
        {
            var result = await _appointmentService.EditAppointmentStatus(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("Room Number")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditAppointmentRoomNumber(int id, int changeTo)
        {
            var result = await _appointmentService.EditAppointmentRoomNumber(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpPatch("Appointment Type")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> EditAppointmentType(int id, string changeTo)
        {
            var result = await _appointmentService.EditAppointmentType(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _appointmentService.DeleteAppointment(id);

            if (result.IsSucced)
                return Ok();
            return
                BadRequest(result.Massage);
        }
        
    }
}
