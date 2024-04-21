namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext _dbContext)
    : IUserRepository
{
    public async Task<PaginatedDataResponse<UserEntity>> GetPaginatedUsersAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder)
    {
        var query = _dbContext.Users
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(u =>
                u.LastName.Contains(searchTerm) ||
                u.FirstName.Contains(searchTerm) ||
                u.MiddleName.Contains(searchTerm) ||
                u.Username.Contains(searchTerm) ||
                u.Email.Contains(searchTerm) ||
                u.Phone.Contains(searchTerm));
        }

        query = sortOrder?.ToLower() == "asc" 
            ? query.OrderBy(x => x.Created) 
            : query.OrderByDescending(x => x.Created);
        
        var list = await query
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.Role)
            .ToListAsync();
        var totalCount = await _dbContext.Users
            .AsNoTracking()
            .CountAsync();
        return new PaginatedDataResponse<UserEntity>(list, totalCount);
    }
    
    public async Task<UserEntity?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<UserEntity?> GetByUsernameAsync(string value)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Username == value);
    }

    public async Task CreateAsync(UserEntity entity)
    {
        await _dbContext.Users.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserEntity entity)
    {
        _dbContext.Users.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> IsExistByUsernameAsync(string value)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Username == value);
    }

    public async Task<bool> IsExistByEmailAsync(string value)
    {
       return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Email == value);
    }

    public async Task<bool> IsExistByPhoneAsync(string value)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Phone == value);
    }
}