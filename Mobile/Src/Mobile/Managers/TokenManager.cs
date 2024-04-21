namespace Mobile.Managers;

public interface ITokenManager
{
    Task<string> GetJwtAsync();
    Task<string> GetRefreshTokenAsync();
    Task<string> GetUserIdAsync(string token);
    Task<string> GetUserRole(string token);
    Task<IResult> SignInAsync(TokenRequest request);
    Task<IResult> RefreshAsync();
    Task<IResult> SignOutAsync();
}

public class TokenManager(
    IHttpClientFactory _factory)
    : ITokenManager
{
    public async Task<string?> GetJwtAsync() =>
        await SecureStorage.GetAsync(Storage.AuthToken);

    public async Task<string?> GetRefreshTokenAsync() =>
        await SecureStorage.GetAsync(Storage.RefreshToken);

    public async Task<string> GetUserIdAsync(string token)
    {
        var authToken = new JwtSecurityToken(token);
        return authToken.Claims.First(c => c.Type == CustomClaimTypes.UserId).Value;
    }

    public async Task<string> GetUserRole(string token)
    {
        var authToken = new JwtSecurityToken(token);
        return authToken.Claims.First(c => c.Type == CustomClaimTypes.Role).Value;
    }

    public async Task<IResult> SignInAsync(TokenRequest request)
    {
        var response = await _factory.CreateClient("mobile_client")
            .PostAsJsonAsync(TokenRoutes.SignIn, request);
        var result = await response.ToResult<TokenResponse>();
        if (!result.Succeeded)
            return await Result.FailAsync(result.Messages);
        await SecureStorage.SetAsync(Storage.AuthToken, result.Data.AuthToken);
        await SecureStorage.SetAsync(Storage.RefreshToken, result.Data.RefreshToken);
        return await Result.SuccessAsync();
    }

    public async Task<IResult> RefreshAsync()
    {
        var authToken = await GetJwtAsync();
        var refreshToken = await GetRefreshTokenAsync();
        var refreshTokenRequest = new RefreshTokenRequest(
            AuthToken: authToken,
            RefreshToken: refreshToken);
        var response = await _factory.CreateClient("mobile_client")
            .PostAsJsonAsync(TokenRoutes.RefreshToken, refreshTokenRequest);
        var result = await response.ToResult<TokenResponse>();
        if (!result.Succeeded)
            return await Result.FailAsync();
        await SecureStorage.SetAsync(Storage.AuthToken, result.Data.AuthToken);
        await SecureStorage.SetAsync(Storage.RefreshToken, result.Data.RefreshToken);
        return await Result.SuccessAsync();
    }

    public async Task<IResult> SignOutAsync()
    {
        SecureStorage.Remove(Storage.AuthToken);
        SecureStorage.Remove(Storage.RefreshToken);
        return await Result.SuccessAsync("Пользователь успешно вышел.");
    }
}