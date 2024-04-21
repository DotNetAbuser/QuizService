namespace Mobile.Views.Main;

public partial class TestsView : ContentPage
{
    public TestsView(TestsVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}