namespace Application.IServices;

public interface IResultService
{
    Task<Result<PaginatedDataResponse<UserTestResultResponse>>> GetPaginatedUserTestResultsByUserIdAsync(string userId,string currentUserId,
        int pageNumber, int pageSize, string searchTerm, string sortOrder);
    Task<Result<PaginatedDataResponse<UserTestResultResponse>>> GetPaginatedUsersTestResultsAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder);
    Task<Result> CreateAsync(CreateTestResultRequest request, string currentUserId);
}