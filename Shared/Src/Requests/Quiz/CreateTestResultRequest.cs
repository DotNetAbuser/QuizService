namespace Shared.Requests.Quiz;

public record CreateTestResultRequest(
    [Required] string UserId, 
    [Required] string TestId, 
    [Required] List<Question> Questions );