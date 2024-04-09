namespace Infrastructure.Repositories;

public class RefreshSessionRepository(
    ApplicationDbContext _dbContext)
    : IRefreshSessionRepository
{
    public async Task<RefreshSessionEntity?> GetByRefreshTokenAsync(string value)
    {
        return await _dbContext.RefreshSessions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Token == value);
    }

    public async Task CreateAsync(RefreshSessionEntity entity)
    {
        await _dbContext.RefreshSessions.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(RefreshSessionEntity entity)
    {
        _dbContext.RefreshSessions.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}