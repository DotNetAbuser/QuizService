namespace Shared.Requests.Identity;

public record UpdateUserRoleRequest(
    [Required] UserRoleModel UserRoles);