using HastaneYonetimSistemiApp.Business.Operations.Doctor.Dto;
using HastaneYonetimSistemiApp.Business.Operations.Patient.Dto;
using HastaneYonetimSistemiApp.Business.Types;
using HastaneYonetimSistemiApp.Data.Entities;
using HastaneYonetimSistemiApp.Data.Enums;
using HastaneYonetimSistemiApp.Data.Repostories;
using HastaneYonetimSistemiApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.Patient
{
    public class PatientManager : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PatientEntity> _repository;

        public PatientManager(IUnitOfWork unitOfWork, IRepository<PatientEntity> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<PatientDto> GetPatient(int id)
        {
            var patient = await _repository.GetAll(x => x.Id == id)
                .Select(x => new PatientDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    Gender = x.Gender,
                }).FirstOrDefaultAsync();
            return patient;
        }

        public async Task<List<PatientDto>> GetAllPatients()
        {
            var patients = await _repository.GetAll()
               .Select(x => new PatientDto
               {
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   BirthDate = x.BirthDate,
                   Gender = x.Gender,
                   
               }).ToListAsync();
            return patients;
        }
        public async Task<ServiceMassage> AddPatient(AddPatientDto patient)
        {
            var hasPatient = _repository.GetAll(x => x.FirstName.ToLower() == patient.FirstName.ToLower() && x.LastName.ToLower() == patient.LastName.ToLower()).Any();

            if (hasPatient)
            {
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Hasta sisteme kayıtlıdır"

                };

            }
            var patientEntity = new PatientEntity
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Gender = patient.Gender,
                BirthDate = DateTime.Now,
            };
            _repository.Add(patientEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Hasta kaydı sırasında bir hata oluştu");
            }

            return new ServiceMassage
            {
                IsSucced = true,
            };
        }
        public async Task<ServiceMassage> EditPatientFirstName(int id, string changeTo)
        {
            var appointment = _repository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen hasta bulunamadı"
                };
            appointment.FirstName = changeTo;

            _repository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Hasta adı değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditPatientLastName(int id, string changeTo)
        {
            var appointment = _repository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen hasta bulunamadı"
                };
            appointment.LastName = changeTo;

            _repository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Hasta soyadı değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditBirthDate(int id, DateTime changeTo)
        {
            var appointment = _repository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen hasta bulunamadı"
                };
            appointment.BirthDate = changeTo;

            _repository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Hasta doğum tarihi değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditGender(int id, Gender changeTo)
        {
            var appointment = _repository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen hasta bulunamadı"
                };
            appointment.Gender = changeTo;

            _repository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Hasta cinsiyet seçimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> DeletePatient(int id)
        {
            var patient = _repository.GetId(id);
            if (patient == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşlenen hasta bulunamadı."
                };
            patient.IsDeleted = true;
            _repository.Update(patient);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Hasta silinme işlemi sırasında bir sorun oluştu.");

            }
            return new ServiceMassage
            {
                IsSucced = true

            };

        }

        public async Task<ServiceMassage> UpdatePatient(UpdatePatientDto patient)
        {
            var patientEntity = _repository.GetId(patient.Id);

            if (patientEntity is null)
            {
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşlenen hasta bulunamadı"
                };


            }
            patientEntity.FirstName = patient.FirstName;
            patientEntity.LastName = patient.LastName;
            patientEntity.BirthDate = patient.BirthDate;
            patientEntity.Gender = patient.Gender;
            _repository.Update(patientEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Hasta güncelleme sırasında bir hata oluştu.");
            }

            return new ServiceMassage
            { IsSucced = true };
        }


    }



}
