namespace Domain.Entities;

public class QuestionEntity : BaseEntity<Guid>
{
    public Guid QuizId { get; set; }
    public string QuestionContent { get; set; }

    public QuizEntity Quiz { get; set; } = null!;
    public List<ResultDetailEntity> ResultDetails { get; set; } = [];
    public List<OptionEntity> Options { get; set; } = [];
}
