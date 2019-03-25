using Cocorico.RazorComponents.Models.Entities.Jwt;
using Cocorico.RazorComponents.Models.Entities.User;
using Cocorico.RazorComponents.Services.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cocorico.RazorComponents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<CocoricoUser> _userManager;

        public TokenController(
            IJwtTokenService jwtTokenService,
            UserManager<CocoricoUser> userManager)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] TokenDto tokenDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(tokenDto.Email);
            var correctUser = await _userManager.CheckPasswordAsync(user, tokenDto.Password);

            return correctUser
                ? (IActionResult)Ok(new { token = GenerateToken(tokenDto.Email) })
                : BadRequest();
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] TokenDto tokenDto)
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
