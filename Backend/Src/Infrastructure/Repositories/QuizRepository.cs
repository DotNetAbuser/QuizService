namespace Infrastructure.Repositories;

public class QuizRepository(ApplicationDbContext dbContext)
    : IQuizRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

}