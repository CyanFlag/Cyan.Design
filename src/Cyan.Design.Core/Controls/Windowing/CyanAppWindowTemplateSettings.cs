using Avalonia;
using Avalonia.Media;

namespace Cyan.Design.Core.Controls.Windowing;

/// <summary>应用窗口模板设置，提供模板绑定所需的标题栏高度、内容边距等状态值</summary>
public sealed class CyanAppWindowTemplateSettings : AvaloniaObject
{
    /// <summary>标题栏高度样式属性</summary>
    public static readonly StyledProperty<double> TitleBarHeightProperty =
        AvaloniaProperty.Register<CyanAppWindowTemplateSettings, double>(nameof(TitleBarHeight), 32d);

    /// <summary>内容边距样式属性</summary>
    public static readonly StyledProperty<Thickness> ContentMarginProperty =
        AvaloniaProperty.Register<CyanAppWindowTemplateSettings, Thickness>(nameof(ContentMargin));

    /// <summary>标题栏内容是否可见样式属性</summary>
    public static readonly StyledProperty<bool> IsTitleBarContentVisibleProperty =
        AvaloniaProperty.Register<CyanAppWindowTemplateSettings, bool>(nameof(IsTitleBarContentVisible));

    /// <summary>窗口图标样式属性</summary>
    public static readonly StyledProperty<IImage?> WindowIconProperty =
        AvaloniaProperty.Register<CyanAppWindowTemplateSettings, IImage?>(nameof(WindowIcon));

    /// <summary>标题栏高度</summary>
    public double TitleBarHeight
    {
        get => GetValue(TitleBarHeightProperty);
        set => SetValue(TitleBarHeightProperty, value);
    }

    /// <summary>内容边距，用于为标题栏预留空间</summary>
    public Thickness ContentMargin
    {
        get => GetValue(ContentMarginProperty);
        set => SetValue(ContentMarginProperty, value);
    }

    /// <summary>标题栏内容是否可见</summary>
    public bool IsTitleBarContentVisible
    {
        get => GetValue(IsTitleBarContentVisibleProperty);
        set => SetValue(IsTitleBarContentVisibleProperty, value);
    }

    /// <summary>窗口图标</summary>
    public IImage? WindowIcon
    {
        get => GetValue(WindowIconProperty);
        set => SetValue(WindowIconProperty, value);
    }
}
