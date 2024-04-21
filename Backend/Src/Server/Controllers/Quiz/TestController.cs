namespace Server.Controllers.Quiz;

[ApiController]
[Route("api/quiz/test")]
public class TestController(
    ITestService _testService)
    : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _testService.GetAllAsync();
        return Ok(response);
    }
    
    [HttpGet("{quizId}/questions")]
    [Authorize]
    public async Task<IActionResult> GetQuestionsByQuizIdAsync(string quizId)
    {
        var response = await _testService
            .GetQuestionsByQuizIdAsync(quizId);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "teacher, admin")]
    public async Task<IActionResult> CreateAsync(CreateTestRequest request)
    {
        return Ok(await _testService.CreateAsync(request));
    }

    [HttpDelete("{quizId}")]
    [Authorize(Roles = "teacher, admin")]
    public async Task<IActionResult> DeleteAsync(string quizId)
    {
        return Ok(await _testService.DeleteAsync(quizId));
    }
}