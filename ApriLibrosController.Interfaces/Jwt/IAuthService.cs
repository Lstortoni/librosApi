using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static ApiLibrosController.Contracts.JwtDto.AuthDto;

namespace ApiLibrosController.Interfaces.Jwt
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto, CancellationToken ct = default);
        Task<CurrentUserDto?> GetCurrentAsync(ClaimsPrincipal user);
    }
}
