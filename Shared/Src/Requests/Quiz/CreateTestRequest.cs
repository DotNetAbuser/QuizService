namespace Shared.Requests.Quiz;

public record CreateTestRequest(
    [Required] string Title,
    [Required] int CorrectAnswersCount,
    [Required] List<Question> Questions);