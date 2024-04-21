namespace Mobile.Controls;

public partial class ProfileButton : Border
{
    public ProfileButton() =>
        InitializeComponent();
    
    public static readonly BindableProperty ImageProperty = BindableProperty.Create(
        propertyName: nameof(ImageActionSource),
        returnType: typeof(string),
        declaringType: typeof(ProfileButton),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty ButtonTitleProperty = BindableProperty.Create(
        propertyName: nameof(ButtonTitle),
        returnType: typeof(string),
        declaringType: typeof(ProfileButton),
        defaultValue: "Empty",
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty ButtonDescriptionProperty = BindableProperty.Create(
        propertyName: nameof(ButtonDescription),
        returnType: typeof(string),
        declaringType: typeof(ProfileButton),
        defaultValue: "Empty",
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(
        propertyName: nameof(ButtonCommand),
        returnType: typeof(ICommand),
        declaringType: typeof(ProfileButton));
    
    public string ImageActionSource
    {
        get => (string)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }
    
    public string ButtonTitle
    {
        get => (string)GetValue(ButtonTitleProperty);
        set => SetValue(ButtonTitleProperty, value);
    }
    
    public string ButtonDescription
    {
        get => (string)GetValue(ButtonDescriptionProperty);
        set => SetValue(ButtonDescriptionProperty, value);
    }

    public ICommand ButtonCommand
    {
        get => (ICommand)GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }
}