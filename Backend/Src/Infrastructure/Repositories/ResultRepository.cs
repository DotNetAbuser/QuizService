namespace Infrastructure.Repositories;

public class ResultRepository(
    ApplicationDbContext _dbContext)
    : IResultRepository
{
    public async Task CreateAsync(ResultEntity entity)
    {
        await _dbContext.Results.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PaginatedDataResponse<ResultEntity>> GetPaginatedResultsByUserIdAsync(Guid userId,
        int pageNumber, int pageSize, string searchTerm, string sortOrder)
    {
        var query = _dbContext.Results
            .AsNoTracking();
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(u =>
                u.Quiz.Title.Contains(searchTerm) ||
                u.User.LastName.Contains(searchTerm) ||
                u.User.FirstName.Contains(searchTerm) ||
                u.User.MiddleName.Contains(searchTerm));
        }

        query = sortOrder?.ToLower() == "asc" 
            ? query.OrderBy(x => x.Created) 
            : query.OrderByDescending(x => x.Created);
        
        var list = await query
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.Quiz)
            .ThenInclude(x => x.Questions)
            .Include(x => x.ResultDetails)
            .Include(x => x.User)
            .Where(x => x.UserId == userId)
            .ToListAsync();
        var totalCount = await _dbContext.Results
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .CountAsync();
        return new PaginatedDataResponse<ResultEntity>(list, totalCount);
    }

    public async Task<PaginatedDataResponse<ResultEntity>> GetPaginatedResultsAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder)
    {
        var query = _dbContext.Results
            .AsNoTracking();
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(u =>
                u.Quiz.Title.Contains(searchTerm) ||
                u.User.LastName.Contains(searchTerm) ||
                u.User.FirstName.Contains(searchTerm) ||
                u.User.MiddleName.Contains(searchTerm));
        }

        query = sortOrder?.ToLower() == "asc" 
            ? query.OrderBy(x => x.Created) 
            : query.OrderByDescending(x => x.Created);
        
        var list = await query
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.ResultDetails)
            .Include(x => x.User)
            .Include(x => x.Quiz)
            .ThenInclude(x => x.Questions)
            .ToListAsync();
        var totalCount = await _dbContext.Results
            .AsNoTracking()
            .CountAsync();
        return new PaginatedDataResponse<ResultEntity>(list, totalCount);
    }
}