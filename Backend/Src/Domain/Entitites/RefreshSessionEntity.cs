namespace Domain.Entities;

public class RefreshSessionEntity : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public string Token { get; set; }
    public DateTime ExpiredDate { get; set; }

    public UserEntity User { get; set; } = null!;
}
