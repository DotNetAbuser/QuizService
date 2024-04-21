namespace Shared.Models;

public class Option
{
    public string QustionId { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string OptionContent { get; set; } = string.Empty;
    public bool IsSelected { get; set; } = false;
    public bool IsRight { get; set; } = false;
}