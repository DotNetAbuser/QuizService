namespace Domain.Entities;

public class ResultDetailEntity : BaseEntity<Guid>
{
    public Guid ResultId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }
    public bool IsRight { get; set; }

    public ResultEntity Result { get; set; } = null!;
    public QuestionEntity Question { get; set; } = null!;
    public OptionEntity Option { get; set; } = null!;
}
