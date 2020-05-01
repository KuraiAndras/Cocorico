using MediatR;

namespace Cocorico.Shared.Api.Users
{
    public class RegisterUser : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordConfirm { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
