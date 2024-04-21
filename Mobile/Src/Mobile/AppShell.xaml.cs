namespace Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        AddRoutes();
    }

    private static void AddRoutes()
    {
        Routing.RegisterRoute(nameof(CreateTestView),
            typeof(CreateTestView));
        Routing.RegisterRoute(nameof(EditUserView),
            typeof(EditUserView));
        Routing.RegisterRoute(nameof(TestsResultsView),
            typeof(TestsResultsView));
        Routing.RegisterRoute(nameof(TestsView),
            typeof(TestsView));
        Routing.RegisterRoute(nameof(ProfileView),
            typeof(ProfileView));
        Routing.RegisterRoute(nameof(StartTestView),
            typeof(StartTestView));
        Routing.RegisterRoute(nameof(TestsPanelView),
            typeof(TestsPanelView));
        Routing.RegisterRoute(nameof(UsersPanelView),
            typeof(UsersPanelView));
        
        Routing.RegisterRoute(nameof(SignInView),
            typeof(SignInView));
        Routing.RegisterRoute(nameof(SignUpView),
            typeof(SignUpView));
        Routing.RegisterRoute(nameof(StartUpView),
            typeof(StartUpView));
    }
}