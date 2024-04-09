namespace Shared.Responses.Identity;

public class UserRolesResponse
{
    public List<UserRoleModel> UserRoles { get; set; } = [];
}

public class UserRoleModel
{
    public string RoleName { get; set; }
    public bool Selected { get; set; }
}