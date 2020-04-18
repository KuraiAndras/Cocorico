using Cocorico.Shared.Api.Authentication;

namespace Cocorico.Shared.Api.Users
{
    public class UserClaimRequest
    {
        public string UserId { get; set; } = string.Empty;
        public CocoricoClaim CocoricoClaim { get; set; } = new CocoricoClaim();
    }
}
