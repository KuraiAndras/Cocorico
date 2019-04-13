namespace Cocorico.Shared.Dtos.Authentication
{
    public class UserClaimRequest
    {
        public string UserId { get; set; }
        public ClaimRequest Claim { get; set; }
    }
}
