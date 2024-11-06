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
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.Doctor
{
    public class DoctorManager : IDoctorService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IRepository<DoctorEntity> _doctorRepository;

        public DoctorManager(IUnitOfWork unitOfWork, IRepository<DoctorEntity> doctorRepository)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
        }

        public async Task<DoctorDto> GetDoctor(int id)
        {
            var doctor = await _doctorRepository.GetAll(x => x.Id == id)
                .Select(x => new DoctorDto
                {
                    DoctorSpeciality = x.DoctorSpeciality,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                }).FirstOrDefaultAsync();
            return doctor;
        }

        public async Task<List<DoctorDto>> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAll()
               .Select(x => new DoctorDto
               {
                   DoctorSpeciality = x.DoctorSpeciality,
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   PhoneNumber = x.PhoneNumber,
               }).ToListAsync();
            return doctors;
        }


        public async Task<ServiceMassage> AddDoctor(AddDoctorDto doctor)
        {
            var hasDoctor = _doctorRepository.GetAll(x => x.FirstName.ToLower() == doctor.FirstName.ToLower() && x.LastName.ToLower() == doctor.LastName.ToLower()).Any();

            if (hasDoctor)
            {
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Doktor sisteme kayıtlıdır.",
                };

            }

            var doctorEntity = new DoctorEntity
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                DoctorSpeciality = doctor.DoctorSpeciality,
                PhoneNumber = doctor.PhoneNumber,
                PoliclinicId = doctor.PoliclinicId
            };
            _doctorRepository.Add(doctorEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Doktor ekleme sırasında bir hata oluştu");
            }

            return new ServiceMassage
            {
                IsSucced = true,
            };
        }

        

        public async Task<ServiceMassage> EditDoctorFirstName(int id, string changeTo)
        {
            var appointment = _doctorRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen doktor bulunamadı"
                };
            appointment.FirstName = changeTo;

            _doctorRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Doktor adı değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditDoctorLastName(int id, string changeTo)
        {
            var appointment = _doctorRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen doktor bulunamadı"
                };
            appointment.LastName = changeTo;

            _doctorRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Doktor soyadı değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditDoctorSpeciality(int id, DoctorSpeciality changeTo)
        {
            var appointment = _doctorRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen doktor bulunamadı"
                };
            appointment.DoctorSpeciality = changeTo;

            _doctorRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Doktor uzmanlık değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditPhoneNumber(int id, string changeTo)
        {
            var appointment = _doctorRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen doktor bulunamadı"
                };
            appointment.PhoneNumber = changeTo;

            _doctorRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Doktor telefon numarsı değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }

        public async Task<ServiceMassage> EditPoliclinicId(int id, int changeTo)
        {
            var appointment = _doctorRepository.GetId(id);
            if (appointment == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen doktor bulunamadı"
                };
            appointment.PoliclinicId = changeTo;

            _doctorRepository.Update(appointment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Doktor poliklinik id değişimi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }
        public async Task<ServiceMassage> DeleteDoctor(int id)
        {
            var doctor = _doctorRepository.GetId(id);
            if (doctor == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşlenen doktor bulunamadı."
                };
            doctor.IsDeleted = true;
            _doctorRepository.Update(doctor);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Doktor silinme işlemi sırasında bir sorun oluştu.");

            }
            return new ServiceMassage
            {
                IsSucced = true

            };
        }

        public async Task<ServiceMassage> UpdateDoctor(UpdateDoctorDto doctor)
        {
            var doctorEntity = _doctorRepository.GetId(doctor.Id);

            if (doctorEntity is null)
            {
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşlenen hasta bulunamadı"
                };


            }
            doctorEntity.FirstName = doctor.FirstName;
            doctorEntity.LastName= doctor.LastName;
            doctorEntity.PhoneNumber = doctor.PhoneNumber;
            doctorEntity.DoctorSpeciality = doctor.DoctorSpeciality;
            doctorEntity.PoliclinicId = doctor.PoliclinicId;

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Doktor güncelleme sırasında bir hata oluştu.");
            }

            return new ServiceMassage
            { IsSucced = true };
        }

        
    }
}
