namespace Mobile.Views.Main;

public partial class TestsResultsView : ContentPage
{
    public TestsResultsView(TestsResultsVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}