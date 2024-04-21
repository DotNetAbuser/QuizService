namespace Application.IServices;

public interface ITestService
{
    Task<Result<IEnumerable<TestResponse>>> GetAllAsync();
    Task<Result<IEnumerable<Question>>> GetQuestionsByQuizIdAsync(string quizId);

    Task<Result> CreateAsync(CreateTestRequest request);
    Task<Result> DeleteAsync(string testId);
}