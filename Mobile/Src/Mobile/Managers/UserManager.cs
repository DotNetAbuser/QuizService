namespace Mobile.Managers;

public interface IUserManager
{
    Task<IResult<PaginatedDataResponse<UserResponse>>> GetPaginatedUsersAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder);  
    
    Task<IResult<UserResponse>> GetByIdAsync(string userId);
    
    Task<IResult> SignUpAsync(SignUpRequest request);
    Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);
    
    Task<IResult<UserRolesResponse>> GetRolesByUserIdAsync(string userId);
    Task<IResult> UpdateRoleAsync(string userId, UpdateUserRoleRequest request);
}

public class UserManager(
    IHttpClientFactory _factory)
    : IUserManager
{
    public async Task<IResult<PaginatedDataResponse<UserResponse>>> GetPaginatedUsersAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder)
    {
        var response = await _factory.CreateClient("mobile_client")
            .GetAsync(UserRoutes.GetPaginatedUsers(pageNumber, pageSize, searchTerm, sortOrder));
        return await response.ToResult<PaginatedDataResponse<UserResponse>>();
    }

    public async Task<IResult<UserResponse>> GetByIdAsync(string userId)
    {
        var response = await _factory.CreateClient("mobile_client")
            .GetAsync(UserRoutes.GetById(userId));
        return await response.ToResult<UserResponse>();
    }

    public async Task<IResult> SignUpAsync(SignUpRequest request)
    {
        var response = await _factory.CreateClient("mobile_client")
            .PostAsJsonAsync(UserRoutes.SingUp, request);
        return await response.ToResult();
    }

    public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
    {
        var response = await _factory.CreateClient("mobile_client")
            .PostAsJsonAsync(UserRoutes.ToggleUserStatus, request);
        return await response.ToResult();
    }

    public async Task<IResult<UserRolesResponse>> GetRolesByUserIdAsync(string userId)
    {
        var response = await _factory.CreateClient("mobile_client")
            .GetAsync(UserRoutes.GetRolesByUserId(userId));
        return await response.ToResult<UserRolesResponse>();
    }

    public async Task<IResult> UpdateRoleAsync(string userId, UpdateUserRoleRequest request)
    {
        var response = await _factory.CreateClient("mobile_client")
            .PutAsJsonAsync(UserRoutes.UpdateRolesByUserId(userId), request);
        return await response.ToResult();
    }
}