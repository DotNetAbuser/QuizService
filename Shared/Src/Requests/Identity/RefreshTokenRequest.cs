namespace Shared.Requests.Identity;

public record RefreshTokenRequest(
    [Required] string AuthToken,
    [Required] string RefreshToken);