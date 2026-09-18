namespace Cyan.Design.Demo.Models;

public sealed class NavItem
{
    public string Header { get; init; } = string.Empty;
    public string IconKey { get; init; } = string.Empty;
    public Func<object> PageFactory { get; init; } = static () => new object();
}
