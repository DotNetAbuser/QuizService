namespace Application.IRepositories;

public interface IQuizRepository
{
    Task<IEnumerable<QuizEntity>> GetAllAsync();
    Task<QuizEntity?> GetByIdAsync(Guid id);
    
    Task<bool> IsExistByTitleAsync(string value);
    Task CreateAsync(QuizEntity entity);
    Task DeleteAsync(QuizEntity entity);
}