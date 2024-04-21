namespace Mobile.Views.Main;

public partial class UsersPanelView : ContentPage
{
    public UsersPanelView(UsersPanelVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}