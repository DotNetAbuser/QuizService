namespace Infrastructure.Repositories;

public class QuestionRepository(
    ApplicationDbContext _dbContext)
    : IQuestionRepository
{
    public async Task<IEnumerable<QuestionEntity>> GetAllByQuizId(Guid QuizId)
    {
        return await _dbContext.Questions
            .AsNoTracking()
            .Include(x => x.Options)
            .Where(x => x.QuizId == QuizId)
            .ToListAsync();
    }
}