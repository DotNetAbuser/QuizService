namespace Application.IServices;

public interface ITokenService
{
    Task<Result<TokenResponse>> SignIn(TokenRequest request);
    Task<Result<TokenResponse>> RefreshToken(RefreshTokenRequest request);
    Task<Result> RevokeToken(RefreshTokenRequest request);
}