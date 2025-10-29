using ApiLibrosController.Interfaces.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ApiLibrosController.Contracts.JwtDto.AuthDto;

namespace ApiLibrosController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto, CancellationToken ct)
        {
            var resp = await _auth.LoginAsync(dto, ct);
            return resp is null ? Unauthorized() : Ok(resp);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var me = await _auth.GetCurrentAsync(User);
            return Ok(me);
        }
    }

}
