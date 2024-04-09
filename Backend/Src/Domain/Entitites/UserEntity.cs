namespace Domain.Entities;

public class UserEntity : BaseEntity<Guid>
{
    public int RoleId { get; set; }

    public string LastName { get; set; } 
    public string FirstName { get; set; } 
    public string? MiddleName { get; set; } 

    public string Username { get; set; } 
    public string PasswordHash { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }

    public bool IsActive { get; set; }

    public RoleEntity Role { get; set; } = null!;
    public List<ResultEntity> Results { get; set; } = [];
    public List<RefreshSessionEntity> RefreshSessions { get; set; } = [];
}
