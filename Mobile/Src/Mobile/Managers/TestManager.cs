namespace Mobile.Managers;

public interface ITestManager
{
    Task<IResult<IEnumerable<TestResponse>>> GetAllAsync();
    Task<IResult<IEnumerable<Question>>> GetTestWithQuestionsByIdAsync(string testId);

    Task<IResult> CreateAsync(CreateTestRequest request);
    Task<IResult> DeleteAsync(string testId);
}

public class TestManager(
    IHttpClientFactory _factory)
    : ITestManager
{
    public async Task<IResult<IEnumerable<TestResponse>>> GetAllAsync()
    {
        var response = await _factory.CreateClient("mobile_client")
            .GetAsync(TestRoutes.GetAll);
        return await response.ToResult<IEnumerable<TestResponse>>();
    }

    public async Task<IResult<IEnumerable<Question>>> GetTestWithQuestionsByIdAsync(string testId)
    {
        var response = await _factory.CreateClient("mobile_client")
            .GetAsync(TestRoutes.GetTestWithQuestionsById(testId));
        return await response.ToResult<IEnumerable<Question>>();
    }

    public async Task<IResult> CreateAsync(CreateTestRequest request)
    {
        var response = await _factory.CreateClient("mobile_client")
            .PostAsJsonAsync(TestRoutes.Create, request);
        return await response.ToResult();
    }

    public async Task<IResult> DeleteAsync(string testId)
    {
        var response = await _factory.CreateClient("mobile_client")
            .DeleteAsync(TestRoutes.Delete(testId));
        return await response.ToResult();
    }
}