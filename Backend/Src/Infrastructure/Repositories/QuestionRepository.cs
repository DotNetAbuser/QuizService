namespace Infrastructure.Repositories;

public class QuestionRepository(ApplicationDbContext dbContext)
    : IQuestionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
}