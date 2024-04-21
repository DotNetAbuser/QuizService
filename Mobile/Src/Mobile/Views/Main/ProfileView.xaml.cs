namespace Mobile.Views.Main;

public partial class ProfileView : ContentPage
{
    public ProfileView(ProfileVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}