namespace Infrastructure.Repositories;

public class ResultRepository(ApplicationDbContext dbContext)
    : IResultRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
}