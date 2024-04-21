namespace Mobile.Views.Main;

public partial class StartTestView : ContentPage
{
    public StartTestView(StartTestVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}