namespace Application.IRepositories;

public interface IQuestionRepository
{
    Task<IEnumerable<QuestionEntity>> GetAllByQuizId(Guid QuizId);
}