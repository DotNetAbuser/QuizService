namespace Mobile.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        return services
            .AddTransient<CreateTestVM>()
            .AddTransient<EditUserVM>()
            .AddTransient<ProfileVM>()
            .AddTransient<StartTestVM>()
            .AddTransient<TestsPanelVM>()
            .AddTransient<TestsResultsVM>()
            .AddTransient<TestsVM>()
            .AddTransient<UsersPanelVM>()

            .AddTransient<SignInVM>()
            .AddTransient<SignUpVM>()
            .AddTransient<StartUpVM>();
    }
    
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        return services
            .AddTransient<CreateTestView>()
            .AddTransient<EditUserView>()
            .AddTransient<ProfileView>()
            .AddTransient<StartTestView>()
            .AddTransient<TestsPanelView>()
            .AddTransient<TestsResultsView>()
            .AddTransient<TestsView>()
            .AddTransient<UsersPanelView>()

            .AddTransient<SignInView>()
            .AddTransient<SignUpView>()
            .AddTransient<StartUpView>();
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAlertService, AlertService>()
            .AddTransient<INavigationService, NavigationService>();
    }

    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        return services
            .AddTransient<IResultManager, ResultManager>()
            .AddTransient<ITestManager, TestManager>()
            .AddTransient<ITokenManager, TokenManager>()
            .AddTransient<IUserManager, UserManager>();
    }

    public static IServiceCollection AddHttpFactory(this IServiceCollection services)
    {
        services
            .AddTransient<AuthenticationHeaderHandler>()
            .AddHttpClient("mobile_client")
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://194.67.67.140:6007/"))
            .AddHttpMessageHandler<AuthenticationHeaderHandler>();
        return services;
    }
}