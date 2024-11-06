using HastaneYonetimSistemiApp.Business.Operations.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HastaneYonetimSistemiApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> ToggleMaintenence()
        {
            await _settingService.ToggleMaintenence();
            return Ok();
        }
    }
}
