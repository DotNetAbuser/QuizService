namespace Application.IServices;

public interface IUserService
{ 
    Task<Result<PaginatedDataResponse<UserResponse>>> GetPaginatedUsersAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder);
    Task<Result<UserResponse>> GetByIdAsync(string userId);
    
    
    Task<Result> SignUpAsync(SignUpRequest request);
    Task<Result> ToggleUserStatusAsync(ToggleUserStatusRequest request);
    
    Task<Result<UserRolesResponse>> GetRolesByUserIdAsync(string userId);
    Task<Result> UpdateRoleAsync(string userId, UpdateUserRoleRequest request, string currentUserId);
}