using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers.Identity;

[ApiController]
[Route("api/identity/user")]
public class UserController(
    IUserService _userService)
    : ControllerBase
{
    [HttpGet]
<<<<<<< HEAD
    [Authorize(Roles = "admin")]
=======
    // [Authorize(Roles = "Admin")]
>>>>>>> origin/main
    public async Task<IActionResult> GetPaginatedUsersAsync(
        int pageNumber, int pageSize, string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var response = await _userService.GetPaginatedUsersAsync(
            pageNumber, pageSize, searchTerm, sortColumn, sortOrder);
        return Ok(response);
    }
        
    [HttpGet("{userId}")]
    // [Authorize]
    public async Task<IActionResult> GetByIdAsync(string userId)
    {
        var response = await _userService.GetByIdAsync(userId);
        return Ok(response);
    }
    
    [HttpGet("role/{userId}")]
    [Authorize (Roles = "admin")]
    public async Task<IActionResult> GetRolesByUserIdAsync(string userId)
    {
        var response = await _userService.GetRolesByUserIdAsync(userId);
        return Ok(response);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpAsync(RegisterRequest request)
    {
        return Ok(await _userService.RegisterUserAsync(request));
    }
    
    [HttpPost("toggle-status")]
    // [Authorize (Roles = "admin")]
    public async Task<IActionResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
    {
        return Ok(await _userService.ToggleUserStatusAsync(request));
    }
    
<<<<<<< HEAD
=======
    [HttpGet("role/{userId}")]
    // [Authorize (Roles = "admin")]
    public async Task<IActionResult> GetRolesByUserIdAsync(string userId)
    {
        var response = await _userService.GetRolesByUserIdAsync(userId);
        return Ok(response);
    }
    
>>>>>>> origin/main
    [HttpPut("role/{userId}")]
    // [Authorize (Roles = "admin")]
    public async Task<IActionResult> UpdateRolesByUserIdAsync(string userId, UpdateUserRoleRequest request)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        return Ok(await _userService.UpdateRoleAsync(userId, request, currentUserId));
    }
}