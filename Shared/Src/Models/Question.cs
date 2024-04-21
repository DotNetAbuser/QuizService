namespace Shared.Models;

public class Question
{
    public string QuizId { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string QuestionContent { get; set; } = string.Empty;
    public List<Option> Options { get; set; } = [];
}