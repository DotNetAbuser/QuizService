namespace Server.Controllers.Quiz.Quiz;

[ApiController]
[Route("api/quiz/result")]
public class ResultController(
    IResultService _resultService)
    : ControllerBase
{
    
    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetPaginatedUserTestResultsByUserIdAsync(string userId,
        int pageNumber, int pageSize, string? searchTerm, string? sortOrder)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        var response = await _resultService.GetPaginatedUserTestResultsByUserIdAsync(userId, currentUserId,
            pageNumber, pageSize, searchTerm, sortOrder);
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "teacher, admin")]
    public async Task<IActionResult> GetPaginatedUsersTestResultsAsync(
        int pageNumber, int pageSize, string? searchTerm, string? sortOrder)
    {
        var response = await _resultService.GetPaginatedUsersTestResultsAsync(
            pageNumber, pageSize, searchTerm, sortOrder);
        return Ok(response);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(CreateTestResultRequest request)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        return Ok(await _resultService.CreateAsync(request, currentUserId));
    }
}