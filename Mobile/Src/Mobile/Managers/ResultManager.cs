namespace Mobile.Managers;

public interface IResultManager
{
    Task<IResult<PaginatedDataResponse<UserTestResultResponse>>> GetPaginatedUserTestResultsByUserIdAsync(string userId,
        int pageNumber, int pageSize, string searchTerm, string sortOrder);
    Task<IResult<PaginatedDataResponse<UserTestResultResponse>>> GetPaginatedUsersTestResultsAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder);
    Task<IResult> CreateAsync(CreateTestResultRequest request);
    
}

public class ResultManager(
    IHttpClientFactory _factory)
    : IResultManager
{
    public async Task<IResult<PaginatedDataResponse<UserTestResultResponse>>> GetPaginatedUserTestResultsByUserIdAsync(string userId, 
        int pageNumber, int pageSize, string searchTerm, string sortOrder)
    {
        var response = await _factory.CreateClient("mobile_client")
            .GetAsync(ResultRoutes.GetPaginatedUserTestResultsByUserId(userId,
                pageNumber, pageSize, searchTerm,sortOrder));
        return await response.ToResult<PaginatedDataResponse<UserTestResultResponse>>();
    }

    public async Task<IResult<PaginatedDataResponse<UserTestResultResponse>>> GetPaginatedUsersTestResultsAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder)
    {
        var response = await _factory.CreateClient("mobile_client")
            .GetAsync(ResultRoutes.GetPaginatedUsersTestResults(
                pageNumber, pageSize, searchTerm, sortOrder));
        return await response.ToResult<PaginatedDataResponse<UserTestResultResponse>>();
    }

    public async Task<IResult> CreateAsync(CreateTestResultRequest request)
    {
        var response = await _factory.CreateClient("mobile_client")
            .PostAsJsonAsync(ResultRoutes.Create, request);
        return await response.ToResult<UserTestResultResponse>();
    }
}