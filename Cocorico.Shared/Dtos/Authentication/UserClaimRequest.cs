namespace Cocorico.Shared.Dtos.Authentication
{
    public class UserClaimRequest
    {
        public string UserId { get; set; } = string.Empty;
        public CocoricoClaim CocoricoClaim { get; set; } = new CocoricoClaim();
    }
}
