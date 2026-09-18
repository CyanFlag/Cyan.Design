using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Cyan.Design.Core.Controls.Dropdowns;

/// <summary>下拉菜单容器控件，纵向排列菜单项</summary>
public class CyanDropdownMenu : StackPanel
{
    /// <summary>是否支持选中样式属性</summary>
    public static readonly StyledProperty<bool> SelectableProperty =
        AvaloniaProperty.Register<CyanDropdownMenu, bool>(nameof(Selectable));

    /// <summary>是否支持菜单项选中</summary>
    public bool Selectable
    {
        get => GetValue(SelectableProperty);
        set => SetValue(SelectableProperty, value);
    }

    /// <summary>初始化 CyanDropdownMenu 的新实例</summary>
    public CyanDropdownMenu()
    {
        Orientation = Orientation.Vertical;
    }
}
