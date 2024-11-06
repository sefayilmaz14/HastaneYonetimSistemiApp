using HastaneYonetimSistemiApp.Business.Operations.Doctor;
using HastaneYonetimSistemiApp.Business.Operations.Patient;
using HastaneYonetimSistemiApp.Business.Operations.User;
using HastaneYonetimSistemiApp.Business.Operations.User.Dtos;
using HastaneYonetimSistemiApp.Data.Enums;
using HastaneYonetimSistemiApp.WebApi.Jwt;
using HastaneYonetimSistemiApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HastaneYonetimSistemiApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUser(id);
            if (user is null)
                return NotFound();
            else
                return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPatients()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("register")]

        public async Task<IActionResult>Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                FirstName = request.FirstName,
                Lastname = request.Lastname,
                Password = request.Password,
            };
            
            var result = await _userService.AddUser(addUserDto);

            if(result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }

      [HttpPost("login")]

        public IActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _userService.LoginUser(new LoginUserDto { Email = request.Email, Password = request.Password });

            if(!result.IsSucced)
                return BadRequest(result.Massage);

            var user = result.Data;
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });

            return Ok(new LoginResponse
            {
                Massage = "Giriş başarı ile gerçekleşti. ",
                Token = token,
            });
        }
        
        [HttpPatch("User Type")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditPoliclinicId(int id, UserType changeTo)
        {
            var result = await _userService.EditUserType(id, changeTo);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Massage);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);

            if (result.IsSucced)
                return Ok();
            return
                BadRequest(result.Massage);
        }


    }
}
