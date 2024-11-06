using HastaneYonetimSistemiApp.Business.Operations.Appointment.Dto;
using HastaneYonetimSistemiApp.Business.Operations.Doctor.Dto;
using HastaneYonetimSistemiApp.Business.Types;
using HastaneYonetimSistemiApp.Data.Entities;
using HastaneYonetimSistemiApp.Data.Enums;
using HastaneYonetimSistemiApp.Data.Repostories;
using HastaneYonetimSistemiApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.Appointment
{
    public class AppointmentManager : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AppointmentsEntity> _appointmentRepository;
        private readonly IRepository<DoctorAppointmentEntity> _doctorAppointmentRepository;
        private readonly IRepository<PatientAppointmentEntity> _patientAppointmentRepository;

        public AppointmentManager(IUnitOfWork unitOfWork, IRepository<AppointmentsEntity> appointmentRepository, IRepository<DoctorAppointmentEntity> doctorAppointmentRepository, IRepository<PatientAppointmentEntity> patientAppointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _doctorAppointmentRepository = doctorAppointmentRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
        }

        public async Task<AppointmentDto> GetAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAll(x => x.Id == id)
               .Select(x => new AppointmentDto
               {
                   AppointmentStatus = x.AppointmentStatus,
                   AppointmentDate = x.AppointmentDate,
                   AppointmentType = x.AppointmentType,
                   DoctorId = x.DoctorId,
                   PatientId = x.PatientId,
                   RoomNumber = x.RoomNumber,
               }).FirstOrDefaultAsync();
            return appointment;
        }

        public async Task<List<AppointmentDto>> GetAllAppointments()
        {
            var appointment = await _appointmentRepository.GetAll()
                .Select(x => new AppointmentDto
                {
                    AppointmentStatus = x.AppointmentStatus,
                    AppointmentDate = x.AppointmentDate,
                    AppointmentType = x.AppointmentType,
                    DoctorId = x.DoctorId,
                    PatientId = x.PatientId,
                    RoomNumber = x.RoomNumber,
                }).ToListAsync();

            return appointment;
        }


        public async Task<List<DoctorAppointmentDto>> GetDoctorAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAll(x => x.DoctorId == id)
               .Select(x => new DoctorAppointmentDto
               {
                   AppointmentStatus = x.AppointmentStatus,
                   AppointmentDate = x.AppointmentDate,
                   AppointmentType = x.AppointmentType,
                   DoctorId = x.DoctorId,
                   PatientId = x.PatientId,
                   RoomNumber = x.RoomNumber,
                   AppointmentId = x.Id
               }).ToListAsync();
            return appointment;
        }

        public async Task<List<PatientAppointmentDto>> GetPatientAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAll(x => x.PatientId == id)
               .Select(x => new PatientAppointmentDto
               {
                  AppointmentDate= x.AppointmentDate,
                  AppointmentType = x.AppointmentType,
                  PatientId = x.PatientId,
                  AppointmentId= x.Id,
                  AppointmentStatus= x.AppointmentStatus,
                  DoctorId= x.DoctorId,
                  RoomNumber= x.RoomNumber,
                  

               }).ToListAsync();
            return appointment;
        }

        public async Task<ServiceMassage> AddAppointment(AddAppointmentDto appointment)
        {
            var hasAppointment = _appointmentRepository.GetAll(x => appointment.PatientId.Contains(x.PatientId) && appointment.DoctorId.Contains(x.DoctorId)).Any();


            if (hasAppointment)
            {
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Randevu sisteme kayıtlıdır.",
                };

            }
            await _unitOfWork.BeginTransaction();


            var appointmentEntity = new AppointmentsEntity
            {
                AppointmentDate = appointment.AppointmentDate,
                AppointmentStatus = appointment.AppointmentStatus,
                AppointmentType = appointment.AppointmentType,
                RoomNumber = appointment.RoomNumber,
                DoctorId = appointment.DoctorId.FirstOrDefault(), // Listenin ilk değerini alır
                PatientId = appointment.PatientId.FirstOrDefault() // Listenin ilk değerini alır
            };

            _appointmentRepository.Add(appointmentEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Randevu kayıt işlemi sırasında bir hata oluştu");

            }

            foreach (var doctorId in appointment.DoctorId)
            {
                var doctorAppointmet = new DoctorAppointmentEntity
                {
                    AppointmentId = appointmentEntity.Id,
                    DoctorId = doctorId,
                };
                _doctorAppointmentRepository.Add(doctorAppointmet);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Randevu kayıt işlemi sırasında bir hata oluştu");

            }

            foreach (var patientId in appointment.PatientId)
            {
                var patientAppointmet = new PatientAppointmentEntity
                {
                    AppointmentId = appointmentEntity.Id,
                    PatientId = patientId,
                };
                _patientAppointmentRepository.Add(patientAppointmet);
            }
            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Randevu kayıt işlemi sırasında bir hata oluştu");

            }

            return new ServiceMassage
            {
                IsSucced = true,
            };
        }



        public async Task<ServiceMassage> EditAppointmentDoctorId(int id, int changeTo)
        {
            var appointment = _appointmentRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen randevu bulunamadı"
                };
            appointment.DoctorId = changeTo;

            _appointmentRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Doktor id değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };

        }

        public async Task<ServiceMassage> EditAppointmentPatientId(int id, int changeTo)
        {
            var appointment = _appointmentRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen randevu bulunamadı"
                };
            appointment.PatientId = changeTo;

            _appointmentRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Hasta id değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }
        public async Task<ServiceMassage> EditAppointmentDate(int id, DateTime changeTo)
        {
            var appointment = _appointmentRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen randevu bulunamadı"
                };
            appointment.AppointmentDate = changeTo;

            _appointmentRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Randevu tarih değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditAppointmentStatus(int id, AppointmentStatus changeTo)
        {
            var appointment = _appointmentRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen randevu bulunamadı"
                };
            appointment.AppointmentStatus = changeTo;

            _appointmentRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Randevu tarih değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditAppointmentRoomNumber(int id, int changeTo)
        {
            var appointment = _appointmentRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen randevu bulunamadı"
                };
            appointment.RoomNumber = changeTo;

            _appointmentRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Randevu tarih değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditAppointmentType(int id, string changeTo)
        {
            var appointment = _appointmentRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen randevu bulunamadı"
                };
            appointment.AppointmentType = changeTo;

            _appointmentRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Randevu tarih değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> DeleteAppointment(int id)
        {
            var patient = _appointmentRepository.GetId(id);
            if (patient == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşlenen randevu bulunamadı."
                };
            patient.IsDeleted = true;
            _appointmentRepository.Update(patient);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Randevu silinme işlemi sırasında bir sorun oluştu.");

            }
            return new ServiceMassage
            {
                IsSucced = true

            };
        }


    }
}
