using KraevedAPI.ClassObjects;
using KraevedAPI.Models;
using KraevedAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace KraevedAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IKraevedService _kraevedService;
        public AuthController(IKraevedService kraevedService)
        {
            _kraevedService = kraevedService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginInDto loginDto) {
            LoginOutDto? result = null;

            try {
                result = await _kraevedService.Login(loginDto);
            }
            catch (HttpResponseException) {
                throw;
            }
            catch(Exception ex) {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> SendSms(String phone)
        {
            Boolean? result = null;
            try {
                result = await _kraevedService.SendSms(phone);
            }
            catch (HttpResponseException) {
                throw;
            }
            catch(Exception ex) {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }

        [HttpPost("registration")]
        public async Task<ActionResult> Register([FromBody] LoginInDto loginDto) {
            UserOutDto? result;
            
            try {
                result = await _kraevedService.Register(loginDto.Email, loginDto.Password);
            }
            catch (HttpResponseException) {
                throw;
            }
            catch(Exception ex) {
                return BadRequest(new { ex.Message });
            }

            return Ok(result);
        }
    }
}