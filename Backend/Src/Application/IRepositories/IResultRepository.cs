namespace Application.IRepositories;

public interface IResultRepository
{
    Task CreateAsync(ResultEntity entity);
    Task<PaginatedDataResponse<ResultEntity>> GetPaginatedResultsByUserIdAsync(Guid userId,
        int pageNumber, int pageSize, string searchTerm, string sortOrder);
    Task<PaginatedDataResponse<ResultEntity>> GetPaginatedResultsAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder);
}