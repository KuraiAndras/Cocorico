using Cocorico.Server.Services.Jwt;
using Cocorico.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cocorico.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public TokenController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] TokenDto tokenDto)
        {
            var token = _jwtTokenService.BuildToken(tokenDto.Email);

            return Ok(new {token});
        }
    }
}
