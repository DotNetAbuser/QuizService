namespace Domain.Entities;

public class OptionEntity : BaseEntity<Guid>
{
    public Guid QuestionId { get; set; }
    public string OptionContent { get; set; }
    public bool IsRight { get; set; }

    public List<ResultDetailEntity> ResultDetails { get; set; } = [];
    public QuestionEntity Question { get; set; } = null!;
}
