namespace Server.Controllers.Identity;

[ApiController]
[Route("api/identity/token")]
public class TokenController(
    ITokenService _tokenService)
    : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> GetTokens(TokenRequest request)
    {
        var response = await _tokenService.SignIn(request);
        return Ok(response);
    }
    
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshTokens(RefreshTokenRequest request)
    {
        var response = await _tokenService.RefreshToken(request);
        return Ok(response);
    }
    
    [HttpPost("revoke-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RevokeToken(RefreshTokenRequest request)
    {
        var response = await _tokenService.RevokeToken(request);
        return Ok(response);
    }
}