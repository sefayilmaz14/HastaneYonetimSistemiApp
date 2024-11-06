using HastaneYonetimSistemiApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.User.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public UserType UserType { get; set; }
    }
}
