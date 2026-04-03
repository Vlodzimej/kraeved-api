using KraevedAPI.Models;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KraevedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class AppSettingsController : ControllerBase
    {
        private readonly IKraevedService _kraevedService;
        public AppSettingsController(IKraevedService kraevedService)
        {
            _kraevedService = kraevedService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppSetting>>> GetAllSettings()
        {
            var result = await _kraevedService.GetAllSettings();
            return Ok(result);
        }

        [HttpGet("{key}")]
        [AllowAnonymous]
        public async Task<ActionResult<AppSetting?>> GetSettingByKey(string key)
        {
            var result = await _kraevedService.GetSettingByKey(key);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpsertSetting([FromBody] SettingRequest request)
        {
            var result = await _kraevedService.UpsertSetting(request.Key, request.Value, request.Description);
            return Ok(result);
        }
    }

    public class SettingRequest
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
