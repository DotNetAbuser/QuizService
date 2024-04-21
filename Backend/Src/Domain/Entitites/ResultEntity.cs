namespace Domain.Entities;

public class ResultEntity : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid QuizId { get; set; }
    public bool IsPassed { get; set; }

    public UserEntity User { get; set; } = null!;
    public QuizEntity Quiz { get; set; } = null!;
    public List<ResultDetailEntity> ResultDetails { get; set; } = [];
}
