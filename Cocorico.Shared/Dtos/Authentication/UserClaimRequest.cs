namespace Cocorico.Shared.Dtos.Authentication
{
    public class UserClaimRequest
    {
        public string UserId { get; set; }
        public CocoricoClaim CocoricoClaim { get; set; }
    }
}
