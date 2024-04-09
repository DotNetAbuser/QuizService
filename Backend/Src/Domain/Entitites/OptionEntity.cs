namespace Domain.Entities;

public class OptionEntity : BaseEntity<Guid>
{
    public Guid QuestionId { get; set; }
    public string Option { get; set; }
    public bool IsRight { get; set; }

    public ResultDetailEntity ResultDetail { get; set; } = null!;
    public QuestionEntity Question { get; set; } = null!;
}
