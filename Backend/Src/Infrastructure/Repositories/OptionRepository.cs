namespace Infrastructure.Repositories;

public class OptionRepository(ApplicationDbContext dbContext)
    : IOptionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
}