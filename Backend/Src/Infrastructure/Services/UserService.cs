using Shared.Responses.Identity;

namespace Infrastructure.Services;

public class UserService(
    IUserRepository _userRepository,
    IRoleRepository _roleRepository,
    IPasswordHasher _passwordHasher)
    : IUserService
{
    public async Task<Result<PaginatedDataResponse<UserResponse>>> GetPaginatedUsersAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder)
    {
        var paginatedUsersEntities = await _userRepository.GetPaginatedUsersAsync(
            pageNumber, pageSize, searchTerm, sortOrder);
        var usersResponse = paginatedUsersEntities.List.Select(userEntity => new UserResponse()
            {
                Id = userEntity.Id.ToString(),
                LastName = userEntity.LastName,
                FirstName = userEntity.FirstName,
                MiddleName = userEntity.MiddleName,
                Username = userEntity.Username,
                Email = userEntity.Email,
                Phone = userEntity.Phone,
                IsActive = userEntity.IsActive,
                Created = userEntity.Created
            }).ToList();

        var paginatedUsersResponse = new PaginatedDataResponse<UserResponse>(
            list: usersResponse, totalCount: paginatedUsersEntities.TotalCount);
        return await Result<PaginatedDataResponse<UserResponse>>
            .SuccessAsync(paginatedUsersResponse, "Список пользователей успешно получен.");
    }
    
    public async Task<Result<UserResponse>> GetByIdAsync(string userId)
    {
        var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(userId));
        if (userEntity == null)
        {
            return await Result<UserResponse>
                .FailAsync("Пользователь не найден!");
        }
        var userResponse = new UserResponse
        {
            Id = userEntity.Id.ToString(),
            LastName = userEntity.LastName,
            FirstName = userEntity.FirstName,
            MiddleName = userEntity.MiddleName,
            Username = userEntity.Username,
            Email = userEntity.Email,
            Phone = userEntity.Phone,
            IsActive = userEntity.IsActive,
            Created = userEntity.Created
        };
        return await Result<UserResponse>
            .SuccessAsync(userResponse, "Пользователь успешно получен.");
    }
    
    public async Task<Result> SignUpAsync(SignUpRequest request)
    {
        var usernameIsExist = await _userRepository
            .IsExistByUsernameAsync(request.Username.ToLower());
        if (usernameIsExist)
        {
            return await Result<string>
                .FailAsync("Пользователь с таким же именем пользователя уже существует!");
        }
        
        var emailIsExist = await _userRepository
            .IsExistByEmailAsync(request.Email.ToLower());
        if (emailIsExist)
        {
            return await Result<string>
                .FailAsync("Пользователь с такой же эл. почтой уже существует!");
        }
        
        var phoneIsExist = await _userRepository
            .IsExistByPhoneAsync(request.Phone);
        if (phoneIsExist)
        {
            return await Result<string>
                .FailAsync("Пользователь с таким же номером телефона уже существует!");
        }

        var userEntity = new UserEntity
        {
            Id = Guid.NewGuid(),
            RoleId = (int)Role.Student,
            LastName = request.LastName,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            Username = request.Username.ToLower(),
            PasswordHash = _passwordHasher.Generate(request.Password),
            Email = request.Email.ToLower(),
            Phone = request.Phone,
            IsActive = true,
            Created = DateTime.UtcNow
        };
        await _userRepository.CreateAsync(userEntity);
        return await Result<string>
            .SuccessAsync("Пользователь успешно зарегестрирован.");
    }
    
    public async Task<Result> ToggleUserStatusAsync(ToggleUserStatusRequest request)
    {
        var userEntity = await _userRepository
            .GetByIdAsync(Guid.Parse(request.UserId));
        if (userEntity == null)
        {
            return await Result<string>
                .FailAsync("Пользователь не найден!");
        }

        userEntity.IsActive = request.ActivateUser;
        userEntity.Updated = DateTime.UtcNow;
        await _userRepository.UpdateAsync(userEntity);
        return await Result<string>
            .SuccessAsync("Статус пользователя успешно изменён.");
    }
    
    public async Task<Result<UserRolesResponse>> GetRolesByUserIdAsync(string userId)
    {
        var rolesEntities = await _roleRepository.GetAllAsync();
        var user = await _userRepository
            .GetByIdAsync(Guid.Parse(userId));
        var userRolesList = rolesEntities
            .Select(roleEntity => new UserRoleModel
            {
                RoleName = roleEntity.Name, 
                Selected = roleEntity.Name == user.Role.Name
            }).ToList();
        var response = new UserRolesResponse{ UserRoles = userRolesList };
        return await Result<UserRolesResponse>
            .SuccessAsync(response, "Роли пользователя успешно получены.");
    }
    
    public async Task<Result> UpdateRoleAsync(string userId, UpdateUserRoleRequest request, string currentUserId)
    {
        var userEntity = await _userRepository
            .GetByIdAsync(Guid.Parse(userId));
        var isDefaultUser = userEntity.Email == "student@example.com" ||
                            userEntity.Email == "teacher@example.com" ||
                            userEntity.Email == "admin@example.com";
        if (isDefaultUser)
        {
            return await Result<string>
                .FailAsync("Это действие невозможно для этого пользователя!");
        }
        var selectedRole = request.UserRoles;
        var role = await _roleRepository.GetByNameAsync(selectedRole.RoleName);
        userEntity.Role = role;
        userEntity.Updated = DateTime.UtcNow;
        await _userRepository.UpdateAsync(userEntity);
        return await Result<string>
            .SuccessAsync("Роли пользователя успешно обновлены.");
    }
}