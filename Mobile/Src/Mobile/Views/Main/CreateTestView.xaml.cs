namespace Mobile.Views.Main;

public partial class CreateTestView : ContentPage
{
    public CreateTestView(CreateTestVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}