namespace Infrastructure.Repositories;

public class ResultDetailRepository(ApplicationDbContext dbContext)
    : IResultDetailRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

}