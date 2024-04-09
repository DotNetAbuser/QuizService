namespace Application.IRepositories;

public interface IUserRepository
{
    Task<PaginatedDataResponse<UserEntity>> GetPaginatedUsersAsync(
        int pageNumber, int pageSize, string searchTerm, string sortColumn, string sortOrder);
    Task<UserEntity?> GetByUserIdAsync(Guid id);
    Task<UserEntity?> GetByUsernameAsync(string value);

    
    Task CreateAsync(UserEntity entity);
    Task UpdateAsync(UserEntity entity);
    
    Task<bool> IsExistByUsernameAsync(string value);
    Task<bool> IsExistByEmailAsync(string value);
    Task<bool> IsExistByPhoneAsync(string value);
}