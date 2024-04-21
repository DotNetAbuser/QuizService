namespace Shared.Responses.Quiz;

public class UserTestResultResponse
{
    public string Id { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int QuestionsCount { get; set; }
    public int CorrectAnswersCount { get; set; }
    public bool IsPassed { get; set; }
    public DateTime Created { get; set; }
}