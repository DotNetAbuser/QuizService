namespace Infrastructure.Services;

public class TokenService(IOptions<JwtOptions> _jwtOptions,
    IUserRepository _userRepository,
    IRefreshSessionRepository _refreshSessionRepository,
    IPasswordHasher _passwordHasher)
    : ITokenService
{
    public async Task<Result<TokenResponse>> SignIn(TokenRequest request)
    {
        var userEntity = await _userRepository
            .GetByUsernameAsync(request.Username.ToLower());
        if (userEntity == null)
        {
            return await Result<TokenResponse>
                .FailAsync("Пользователя с данным логином не существует!");
        }

        var isCorrectPassword = _passwordHasher
            .Verify(request.Password, userEntity.PasswordHash);
        if (!isCorrectPassword)
        {
            return await Result<TokenResponse>
                .FailAsync("Неправильный пароль!");
        }

        var isUserActive = userEntity.IsActive;
        if (!isUserActive)
        {
            return await Result<TokenResponse>
                .FailAsync("Профиль пользователя неактивен! " +
                           "Обратитесь в поддержку для выяснения причины блокировки.");
        }
        
        var authToken = generateJwtToken(userEntity);
        var refreshToken = generateRefreshToken();
        var refreshSessionEntity = new RefreshSessionEntity
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            Token = refreshToken,
            ExpiredDate = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow
        };

        await _refreshSessionRepository
            .CreateAsync(refreshSessionEntity);
        var tokenResponse = new TokenResponse
        {
            AuthToken = authToken,
            RefreshToken = refreshToken,
        };

        return await Result<TokenResponse>
            .SuccessAsync(tokenResponse, "Пользователь успешно прошёл аунтификацию!");
    }

    public async Task<Result<TokenResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var refreshSessionEntity = await _refreshSessionRepository
            .GetByRefreshTokenAsync(request.RefreshToken);
        if (refreshSessionEntity == null)
        {
            return await Result<TokenResponse>
                .FailAsync("Сессии не существует, необходимо пройти аунтификацию!");
        }
        if (refreshSessionEntity.ExpiredDate < DateTime.Now)
        {
            await _refreshSessionRepository.DeleteAsync(refreshSessionEntity);
            return await Result<TokenResponse>
                .FailAsync("Сессия устарела, необходимо вновь пройти аунтификацию!");
        }

        var userEntity = await _userRepository.GetByUserIdAsync(refreshSessionEntity.UserId);
        if (userEntity == null)
        {
            await _refreshSessionRepository.DeleteAsync(refreshSessionEntity);
            return await Result<TokenResponse>
                .FailAsync("Пользователя не найден!");
        }
        
        var newAuthToken = generateJwtToken(userEntity);
        var newRefreshToken = generateRefreshToken();
        var newRefreshSessionEntity = new RefreshSessionEntity
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            Token = newRefreshToken,
            ExpiredDate = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow
        };
        await _refreshSessionRepository
            .CreateAsync(newRefreshSessionEntity);

        var tokenResponse = new TokenResponse
        {
            AuthToken = newAuthToken,
            RefreshToken = newRefreshToken,
        };
        
        return await Result<TokenResponse>
            .SuccessAsync(tokenResponse, "Новый пара токенов успешно получены.");
    }

    public async Task<Result> RevokeToken(RefreshTokenRequest request)
    {
        var refreshSession = await _refreshSessionRepository
            .GetByRefreshTokenAsync(request.RefreshToken);
        if (refreshSession == null)
        {
            return await Result<string>
                .FailAsync("Сессии уже существует, " +
                           "необходимо вновь пройти аунтификацию!");
        }
        
        await _refreshSessionRepository
            .DeleteAsync(refreshSession);
        return await Result<string>
            .SuccessAsync("Сессия успешно отозвана!");
    }
    
    private string generateJwtToken(UserEntity user) =>
        generateEncryptedToken(getSigningCredentials(), getClaims(user));

    private IEnumerable<Claim> getClaims(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, user.Id.ToString()),
            new(CustomClaimTypes.Role, user.Role.Name)
        };

        return claims;
    }

    private string generateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var expiresMinutes = _jwtOptions.Value.Expires;
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiresMinutes),
            signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var encryptedToken = tokenHandler.WriteToken(token);

        return encryptedToken;
    }

    private string generateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private SigningCredentials getSigningCredentials()
    {
        var secretKey = _jwtOptions.Value.SecretKey;
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        return new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
    }
}