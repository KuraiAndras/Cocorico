using Cocorico.Server.Models.Entities.User;
using Cocorico.Server.Services.Jwt;
using Cocorico.Shared.Dtos.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cocorico.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<CocoricoUser> _userManager;

        public AuthenticationController(
            IJwtTokenService jwtTokenService,
            UserManager<CocoricoUser> userManager)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginDetails tokenDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(tokenDto.Email);
            var correctUser = await _userManager.CheckPasswordAsync(user, tokenDto.Password);

            return correctUser
                ? (IActionResult)Ok(new { token = GenerateToken(tokenDto.Email) })
                : BadRequest();
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] LoginDetails tokenDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _userManager.CreateAsync(
                new CocoricoUser
                {
                    UserName = tokenDto.Email,
                    Email = tokenDto.Email,
                    Name = tokenDto.Email,
                },
                tokenDto.Password);

            return result.Succeeded ? Ok() : StatusCode(500);
        }

        private string GenerateToken(string email) => _jwtTokenService.BuildToken(email);
    }
}
