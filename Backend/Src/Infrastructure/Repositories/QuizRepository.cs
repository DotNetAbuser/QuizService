namespace Infrastructure.Repositories;

public class QuizRepository(
    ApplicationDbContext _dbContext)
    : IQuizRepository
{
    
    public async Task<IEnumerable<QuizEntity>> GetAllAsync()
    {
        return await _dbContext.Quizes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<QuizEntity?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Quizes
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> IsExistByTitleAsync(string value)
    {
        return await _dbContext.Quizes
            .AsNoTracking()
            .AnyAsync(x => x.Title == value);
    }

    public async Task CreateAsync(QuizEntity entity)
    {
        await _dbContext.Quizes.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(QuizEntity entity)
    {
        _dbContext.Quizes.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}