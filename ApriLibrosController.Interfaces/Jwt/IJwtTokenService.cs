using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApiLibrosController.Contracts.JwtDto.AuthDto;

namespace ApiLibrosController.Interfaces.Jwt
{
    public interface IJwtTokenService
    {
        string GenerateToken(CurrentUserDto user);
    }
}
