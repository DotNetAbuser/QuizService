namespace Application.IRepositories;

public interface IRefreshSessionRepository
{
    Task<RefreshSessionEntity?> GetByRefreshTokenAsync(string value);
    
    Task CreateAsync(RefreshSessionEntity entity);
    Task DeleteAsync(RefreshSessionEntity entity);
}