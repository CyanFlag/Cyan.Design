using Avalonia;

namespace Cyan.Design.Core.Controls.Navigation;

/// <summary>导航视图项控件，表示导航菜单中的一个条目</summary>
public class CyanNavigationViewItem : AvaloniaObject
{
    /// <summary>标题样式属性</summary>
    public static readonly StyledProperty<object?> HeaderProperty =
        AvaloniaProperty.Register<CyanNavigationViewItem, object?>(nameof(Header));

    /// <summary>内容样式属性</summary>
    public static readonly StyledProperty<object?> ContentProperty =
        AvaloniaProperty.Register<CyanNavigationViewItem, object?>(nameof(Content));

    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanNavigationViewItem, object?>(nameof(Icon));

    /// <summary>标题</summary>
    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>内容</summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>图标</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}
