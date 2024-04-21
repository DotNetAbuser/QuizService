namespace Mobile.Controls;

public partial class BorderNumericEntry : VerticalStackLayout
{
    public BorderNumericEntry() =>
        InitializeComponent();
    
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        propertyName: nameof(Placeholder),
        returnType: typeof(string),
        declaringType: typeof(BorderNumericEntry),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty CountProperty = BindableProperty.Create(
        propertyName: nameof(Count),
        returnType: typeof(int),
        declaringType: typeof(BorderNumericEntry),
        defaultValue: 0,
        defaultBindingMode: BindingMode.TwoWay);
    
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
    
    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    private void IncreaseBtn_OnClicked(object? sender, EventArgs e) => Count++;
    

    private void DecreaseBtn_OnClicked(object? sender, EventArgs e) => Count--;
    
}