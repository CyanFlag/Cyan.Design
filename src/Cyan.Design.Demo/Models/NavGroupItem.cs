namespace Cyan.Design.Demo.Models;

public sealed class NavGroupItem
{
    public string GroupName { get; init; } = string.Empty;
    public string IconKey { get; init; } = string.Empty;
    public IReadOnlyList<NavItem> Items { get; init; } = Array.Empty<NavItem>();
}
