using HastaneYonetimSistemiApp.Business.DataProtection;
using HastaneYonetimSistemiApp.Business.Operations.Patient.Dto;
using HastaneYonetimSistemiApp.Business.Operations.User.Dtos;
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

namespace HastaneYonetimSistemiApp.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _protector;


        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _protector = protector;
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAll()
             .Select(x => new UserDto
             {
                 Email = x.Email,
                 FirstName = x.FirstName,
                 Lastname = x.Lastname,
                 UserType = x.UserType

             }).ToListAsync();
            return users;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _userRepository.GetAll(x => x.Id == id)
                .Select(x => new UserDto
                {
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Lastname = x.Lastname,
                    UserType = x.UserType,
                }).FirstOrDefaultAsync();
            return user;
           
        }

        public async Task<ServiceMassage> AddUser(AddUserDto user)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());

            if (hasMail.Any())
            {
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Email adresi zaten mevcut."
                };

            }

            var userEntity = new UserEntity()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Lastname = user.Lastname,
                Password = _protector.Protect(user.Password),
                UserType = UserType.Patient,
            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kullanıcı kaydı sırasında bir hata oluştu.");

            }

            return new ServiceMassage
            {
                IsSucced = true
            };
        }



        public ServiceMassage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());
            if (userEntity is null)
            {
                return new ServiceMassage<UserInfoDto>
                {
                    IsSucced = false,
                    Massage = "Kullanıcı adı veya şifre hatalı."
                };
            }

            var unprotectedPassword = _protector.UnProtect(userEntity.Password);
            if (unprotectedPassword == user.Password)
            {
                return new ServiceMassage<UserInfoDto>
                {
                    IsSucced = true,
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.Lastname,
                        UserType = userEntity.UserType,
                    }
                };
            }
            else
            {
                return new ServiceMassage<UserInfoDto>
                {
                    IsSucced = false,
                    Massage = "Kullanıcı adı veya şifre hatalı."
                };
            }
        }

        public async Task<ServiceMassage> DeleteUser(int id)
        {
            var user = _userRepository.GetId(id);
            if (user == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşlenen kullanıcı bulunamadı."
                };
            user.IsDeleted = true;
            _userRepository.Update(user);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kullanıcı silinme işlemi sırasında bir sorun oluştu.");

            }
            return new ServiceMassage
            {
                IsSucced = true

            };
        }

        public async Task<ServiceMassage> EditUserType(int id, UserType changeTo)
        {
            var user = _userRepository.GetId(id);
            if (user == null)
                return new ServiceMassage
                {
                    IsSucced = false,
                    Massage = "Bu id ile eşleşen kullanıcı bulunamadı"
                };
            user.UserType = changeTo;

            _userRepository.Update(user);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kullanıcı yetkilendirmesi sırasında bir sorun oluştu.");
            }

            return new ServiceMassage { IsSucced = true, };
        }
    }
}

