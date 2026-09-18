using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Cyan.Design.Core.Controls.Layout;

/// <summary>卡片容器控件，支持标题、额外内容、边框、悬浮及阴影效果</summary>
public class CyanCard : ContentControl
{
    /// <summary>卡片头部内容样式属性</summary>
    public static readonly StyledProperty<object?> HeaderProperty =
        AvaloniaProperty.Register<CyanCard, object?>(nameof(Header));

    /// <summary>卡片额外内容样式属性</summary>
    public static readonly StyledProperty<object?> ExtraProperty =
        AvaloniaProperty.Register<CyanCard, object?>(nameof(Extra));

    /// <summary>是否显示边框样式属性</summary>
    public static readonly StyledProperty<bool> BorderedProperty =
        AvaloniaProperty.Register<CyanCard, bool>(nameof(Bordered), true);

    /// <summary>是否可悬浮样式属性</summary>
    public static readonly StyledProperty<bool> HoverableProperty =
        AvaloniaProperty.Register<CyanCard, bool>(nameof(Hoverable));

    /// <summary>卡片阴影样式属性</summary>
    public static readonly StyledProperty<BoxShadows> BoxShadowProperty =
        AvaloniaProperty.Register<CyanCard, BoxShadows>(nameof(BoxShadow));

    /// <summary>卡片头部内容</summary>
    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>卡片额外内容，通常显示在头部右侧</summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }

    /// <summary>是否显示边框</summary>
    public bool Bordered
    {
        get => GetValue(BorderedProperty);
        set => SetValue(BorderedProperty, value);
    }

    /// <summary>鼠标悬浮时是否提升阴影效果</summary>
    public bool Hoverable
    {
        get => GetValue(HoverableProperty);
        set => SetValue(HoverableProperty, value);
    }

    /// <summary>卡片阴影</summary>
    public BoxShadows BoxShadow
    {
        get => GetValue(BoxShadowProperty);
        set => SetValue(BoxShadowProperty, value);
    }
}
