namespace Domain.Entities;

public class QuizEntity : BaseEntity<Guid>
{
    public string Title { get; set; }
    public int CorrectAnswersCount { get; set; }

    public List<ResultEntity> Results { get; set; } = [];
    public List<QuestionEntity> Questions { get; set; } = [];
}
