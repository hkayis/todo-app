using Microsoft.AspNetCore.Mvc;
using TodoApi.Application.CommandHandlers;
using TodoApi.Application.DTOs;

namespace TodoApi.Application.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterHandler _registerHandler;
        private readonly LoginHandler _loginHandler;

        public AuthController(RegisterHandler registerHandler, LoginHandler loginHandler)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var command = new RegisterCommand
            {
                Username = dto.Username,
                Password = dto.Password
            };

            var basarili = await _registerHandler.Handle(command);

            if (!basarili)
                return BadRequest(new { message = "Bu kullanıcı adı zaten alınmış" });

            return Ok(new { message = "Kayıt başarılı" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var command = new LoginCommand
            {
                Username = dto.Username,
                Password = dto.Password
            };

            var sonuc = await _loginHandler.Handle(command);

            if (sonuc == null)
                return Unauthorized("Kullanıcı adı veya şifre hatalı");

            return Ok(sonuc);   
        }
    }
}