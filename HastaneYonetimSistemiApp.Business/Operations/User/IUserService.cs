using HastaneYonetimSistemiApp.Business.Operations.Patient.Dto;
using HastaneYonetimSistemiApp.Business.Operations.User.Dtos;
using HastaneYonetimSistemiApp.Business.Types;
using HastaneYonetimSistemiApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.User
{
    public interface IUserService
    {
        Task<UserDto> GetUser(int id);
        Task<List<UserDto>> GetAllUsers();
        Task<ServiceMassage> AddUser(AddUserDto user);
        Task<ServiceMassage> EditUserType(int id, UserType changeTo);
        ServiceMassage<UserInfoDto> LoginUser(LoginUserDto user);
        Task<ServiceMassage> DeleteUser(int id);
    }
}
