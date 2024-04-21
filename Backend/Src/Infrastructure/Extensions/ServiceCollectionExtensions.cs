namespace Infrastructure;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        return services
            .AddTransient<IPasswordHasher, PasswordHasher>();
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IOptionRepository, OptionRepository>()
            .AddTransient<IQuestionRepository, QuestionRepository>()
            .AddTransient<IQuizRepository, QuizRepository>()
            .AddTransient<IRefreshSessionRepository, RefreshSessionRepository>()
            .AddTransient<IResultDetailRepository, ResultDetailRepository>()
            .AddTransient<IResultRepository, ResultRepository>()
            .AddTransient<IRoleRepository, RoleRepository>()
            .AddTransient<IUserRepository, UserRepository>();
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAccountService, AccountService>()
            .AddTransient<ITokenService, TokenService>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<ITestService, TestService>()
            .AddTransient<IResultService, ResultService>();

    }

}
