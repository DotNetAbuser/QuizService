namespace Application;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<RefreshSessionEntity> RefreshSessions { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<OptionEntity> Options { get; set; }
    public DbSet<QuestionEntity> Questions { get; set; }
    public DbSet<QuizEntity> Quizes { get; set; }
    public DbSet<ResultEntity> Results { get; set; }
    public DbSet<ResultDetailEntity> ResultDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshSessionConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        modelBuilder.ApplyConfiguration(new OptionConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new QuizConfiguration());
        modelBuilder.ApplyConfiguration(new ResultDetailConfiguration());
        modelBuilder.ApplyConfiguration(new ResultConfiguration());
    }
}
