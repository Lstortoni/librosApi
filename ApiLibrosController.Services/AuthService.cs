using ApiLibrosController.Contracts.JwtDto;
using ApiLibrosController.Interfaces.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static ApiLibrosController.Contracts.JwtDto.AuthDto;
using Microsoft.Extensions.Options;

namespace ApiLibrosController.Services
{
    public class AuthService:IAuthService
    {
        private readonly IJwtTokenService _jwt;

        private readonly IOptions<JwtOptions> _opt;


        public AuthService(IJwtTokenService jwt, IOptions<JwtOptions> opt)
        {
            _jwt = jwt;
            _opt = opt;
        }

        public Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto, CancellationToken ct = default)
        {
            //  LÓGICA HARDCODEADA TEMPORAL:
            // Ej. “leo@demo.com” con “123” es Vendedor, “user@demo.com” con “123” es Usuario
            CurrentUserDto? user = dto switch
            {
                { Email: "leo@demo.com", Password: "123" } => new(1, dto.Email, "Leo", new[] { "Vendedor" }),
                { Email: "user@demo.com", Password: "123" } => new(2, dto.Email, "Leo usu comun", new[] { "Usuario" }),
                _ => null
            };

            if (user is null) return Task.FromResult<LoginResponseDto?>(null);

            var token = _jwt.GenerateToken(user);

            var expires = DateTime.UtcNow.AddMinutes(_opt.Value.ExpMinutes);

            return Task.FromResult<LoginResponseDto?>(new(token, expires, user.Name));
        }

        public Task<CurrentUserDto?> GetCurrentAsync(ClaimsPrincipal principal)
        {
            if (principal?.Identity is null || !principal.Identity.IsAuthenticated)
                return Task.FromResult<CurrentUserDto?>(null);

            var id = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var email = principal.FindFirst(ClaimTypes.Email)?.Value ?? "";
            var name = principal.FindFirst(ClaimTypes.Name)?.Value ?? "";
            var roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();

            return Task.FromResult<CurrentUserDto?>(new(id, email, name, roles));
        }
    }
}
