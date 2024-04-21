namespace Mobile.Views.Start;

public partial class SignInView : ContentPage
{
    public SignInView(SignInVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}