using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;

namespace Cyan.Design.Core.Controls.Windowing;

public partial class CyanAppWindow
{
    /// <summary>模板设置样式属性</summary>
    public static readonly StyledProperty<CyanAppWindowTemplateSettings> TemplateSettingsProperty =
        AvaloniaProperty.Register<CyanAppWindow, CyanAppWindowTemplateSettings>(nameof(TemplateSettings));

    /// <summary>窗口图标样式属性</summary>
    public static readonly new StyledProperty<IImage?> IconProperty =
        AvaloniaProperty.Register<CyanAppWindow, IImage?>(nameof(Icon));

    /// <summary>标题栏内容样式属性</summary>
    public static readonly StyledProperty<object?> TitleBarContentProperty =
        AvaloniaProperty.Register<CyanAppWindow, object?>(nameof(TitleBarContent));

    /// <summary>窗口圆角样式属性</summary>
    public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
        AvaloniaProperty.Register<CyanAppWindow, CornerRadius>(nameof(CornerRadius), new CornerRadius(8));

    /// <summary>模板设置，提供窗口模板绑定所需的状态值</summary>
    public CyanAppWindowTemplateSettings TemplateSettings
    {
        get => GetValue(TemplateSettingsProperty);
        private set => SetValue(TemplateSettingsProperty, value);
    }

    /// <summary>窗口图标</summary>
    public new IImage? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>标题栏自定义内容</summary>
    public object? TitleBarContent
    {
        get => GetValue(TitleBarContentProperty);
        set => SetValue(TitleBarContentProperty, value);
    }

    /// <summary>窗口圆角半径</summary>
    public CornerRadius CornerRadius
    {
        get => GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>是否以对话框形式显示窗口（隐藏尺寸按钮）</summary>
    public bool ShowAsDialog
    {
        get => _hideSizeButtons;
        set
        {
            _hideSizeButtons = value;
            if (value)
                PseudoClasses.Add(":dialog");
            else
                PseudoClasses.Remove(":dialog");
        }
    }


    /// <summary>窗口标题栏配置</summary>
    public CyanAppWindowTitleBar TitleBar => _titleBar;

    /// <summary>窗口背景材质样式属性</summary>
    public static readonly StyledProperty<WindowBackdrop> BackdropProperty =
        AvaloniaProperty.Register<CyanAppWindow, WindowBackdrop>(nameof(Backdrop));

    /// <summary>窗口背景材质类型</summary>
    public WindowBackdrop Backdrop
    {
        get => GetValue(BackdropProperty);
        set => SetValue(BackdropProperty, value);
    }

    private bool _hideSizeButtons;
}
