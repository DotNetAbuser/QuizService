namespace Mobile.Views.Main;

public partial class TestsPanelView : ContentPage
{
    public TestsPanelView(TestsPanelVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}