namespace Domain.Entities;

public class QuizEntity : BaseEntity<Guid>
{
    public int Index { get; set; }
    public string Title { get; set; }

    public List<ResultEntity> Results { get; set; } = [];
    public List<QuestionEntity> Questions { get; set; } = [];
}
