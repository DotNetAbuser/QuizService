namespace Shared.Responses.Identity;

public class TokenResponse
{
    [Required] public string AuthToken { get; set; }
    [Required] public string RefreshToken { get; set; }
}