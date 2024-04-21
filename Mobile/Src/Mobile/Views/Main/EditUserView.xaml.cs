namespace Mobile.Views.Main;

public partial class EditUserView : ContentPage
{
    public EditUserView(EditUserVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}