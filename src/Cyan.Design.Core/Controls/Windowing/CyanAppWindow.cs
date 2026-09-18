using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Windowing;

/// <summary>应用窗口控件，提供自定义标题栏、圆角与背景材质等增强外观能力</summary>
public partial class CyanAppWindow : Window
{
    /// <summary>初始化 <see cref="CyanAppWindow"/> 的新实例</summary>
    public CyanAppWindow()
    {
        TemplateSettings = new CyanAppWindowTemplateSettings();
        _titleBar = new CyanAppWindowTitleBar(this);

        if (OperatingSystem.IsWindows() && !Avalonia.Controls.Design.IsDesignMode)
        {
            ExtendClientAreaToDecorationsHint = true;
            WindowDecorations = WindowDecorations.None;

            ExtendClientAreaTitleBarHeightHint = -1;
            IsWindows = true;
        }
    }

    static CyanAppWindow()
    {
        if (OperatingSystem.IsWindows())
            ExtendClientAreaToDecorationsHintProperty.OverrideDefaultValue<CyanAppWindow>(true);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (IsWindows && !Avalonia.Controls.Design.IsDesignMode)
        {
            _defaultTitleBar = e.NameScope.Find<Panel>("DefaultTitleBar");
            _rootBorder = e.NameScope.Find<Border>("RootBorder");
            OnTitleBarHeightChanged(_titleBar.Height);
            SetTitleBarColors();
        }

        if (_defaultTitleBar is not null)
            _defaultTitleBar.PointerPressed += OnTitleBarPointerPressed;

        if (e.NameScope.Find<Button>("MinimizeButton") is { } minBtn)
            minBtn.Click += OnMinimizeClick;
        if (e.NameScope.Find<Button>("MaximizeButton") is { } maxBtn)
            maxBtn.Click += OnMaximizeClick;
        if (e.NameScope.Find<Button>("RestoreButton") is { } restoreBtn)
            restoreBtn.Click += OnMaximizeClick;
        if (e.NameScope.Find<Button>("CloseButton") is { } closeBtn)
            closeBtn.Click += OnCloseClick;
    }

    private void OnTitleBarPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (e.Source is Button) return;
        if (WindowState == WindowState.Maximized) return;
        BeginMoveDrag(e);
    }

    private void OnMinimizeClick(object? sender, RoutedEventArgs e)
        => WindowState = WindowState.Minimized;

    private void OnMaximizeClick(object? sender, RoutedEventArgs e)
        => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

    private void OnCloseClick(object? sender, RoutedEventArgs e)
        => Close();

    /// <summary>窗口打开事件处理</summary>
    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        ApplyBackdropAndDarkMode();
    }

    /// <summary>属性改变事件处理</summary>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IconProperty)
        {
            if (change.NewValue is Bitmap bmp)
                base.Icon = new WindowIcon(bmp);
            if (change.NewValue != null)
                PseudoClasses.Add(s_pcIcon);
            else
                PseudoClasses.Remove(s_pcIcon);
        }
        else if (change.Property == ActualThemeVariantProperty)
        {
            SetTitleBarColors();
            ApplyBackdropAndDarkMode();
        }
        else if (change.Property == BackdropProperty)
        {
            ApplyBackdropAndDarkMode();
        }
    }

    internal void OnExtendsContentIntoTitleBarChanged(bool isExtended)
    {
        if (isExtended)
        {
            TemplateSettings.IsTitleBarContentVisible = false;
            TemplateSettings.ContentMargin = new Thickness();
            PseudoClasses.Add(":extendsTitleBar");
        }
        else
        {
            TemplateSettings.IsTitleBarContentVisible = true;
            TemplateSettings.ContentMargin = new Thickness(0, _titleBar.Height, 0, 0);
            PseudoClasses.Remove(":extendsTitleBar");
        }
    }

    internal void OnTitleBarHeightChanged(double height)
    {
        TemplateSettings.TitleBarHeight = height;
        OnExtendsContentIntoTitleBarChanged(_titleBar.ExtendsContentIntoTitleBar);
    }

    internal void TitleBarColorsChanged()
    {
        SetTitleBarColors();
    }

    internal void OnShowFullScreenButtonChanged(bool value)
    {
        if (!value)
            PseudoClasses.Add(":noFullScreen");
        else
            PseudoClasses.Remove(":noFullScreen");
    }

    private void ApplyBackdropAndDarkMode()
    {
        if (TryGetPlatformHandle() is null) return;

        WindowBackdropHelper.ApplyBackdrop(this, Backdrop);
        WindowBackdropHelper.ApplyDarkMode(this, ActualThemeVariant == ThemeVariant.Dark);

        if (IsWindows && !Avalonia.Controls.Design.IsDesignMode)
            Background = Brushes.Transparent;

        if (Backdrop != WindowBackdrop.None)
        {
            if (_rootBorder is not null)
                _rootBorder.Background = Brushes.Transparent;
        }
        else
        {
            var bg = Application.Current?.TryFindResource("ColorBgLayoutBrush", out var brush) == true && brush is IBrush b
                ? b
                : Brushes.White;
            if (_rootBorder is not null)
                _rootBorder.Background = bg;
        }
    }

    private void SetTitleBarColors()
    {
        if (_titleBar == null)
            return;

        SetResource(s_TitleBarBackground, _titleBar.BackgroundColor);
        SetResource(s_TitleBarForeground, _titleBar.ForegroundColor);
        SetResource(s_TitleBarInactiveBackground, _titleBar.InactiveBackgroundColor);
        SetResource(s_TitleBarInactiveForeground, _titleBar.InactiveForegroundColor);
        SetResource(s_SysCaptionBackground, _titleBar.ButtonBackgroundColor);
        SetResource(s_SysCaptionForeground, _titleBar.ButtonForegroundColor);
        SetResource(s_SysCaptionBackgroundHover, _titleBar.ButtonHoverBackgroundColor);
        SetResource(s_SysCaptionForegroundHover, _titleBar.ButtonHoverForegroundColor);
        SetResource(s_SysCaptionBackgroundPressed, _titleBar.ButtonPressedBackgroundColor);
        SetResource(s_SysCaptionForegroundPressed, _titleBar.ButtonPressedForegroundColor);
        SetResource(s_SysCaptionBackgroundInactive, _titleBar.ButtonInactiveBackgroundColor);
        SetResource(s_SysCaptionForegroundInactive, _titleBar.ButtonInactiveForegroundColor);

        void SetResource(string name, Color? color)
        {
            if (color.HasValue)
                Resources[name] = new SolidColorBrush(color.Value);
            else
                Resources.Remove(name);
        }
    }

    /// <summary>是否运行在 Windows 平台</summary>
    protected internal bool IsWindows { get; internal set; }

    /// <summary>控件样式键重写</summary>
    protected override Type StyleKeyOverride => typeof(CyanAppWindow);

    private Panel? _defaultTitleBar;
    private Border? _rootBorder;
    private readonly CyanAppWindowTitleBar _titleBar;

    private static readonly string s_pcIcon = ":icon";
    private static readonly string s_TitleBarBackground = "TitleBarBackground";
    private static readonly string s_TitleBarForeground = "TitleBarForeground";
    private static readonly string s_TitleBarInactiveBackground = "TitleBarBackgroundInactive";
    private static readonly string s_TitleBarInactiveForeground = "TitleBarForegroundInactive";
    private static readonly string s_SysCaptionBackground = "CaptionButtonBackground";
    private static readonly string s_SysCaptionForeground = "CaptionButtonForeground";
    private static readonly string s_SysCaptionBackgroundHover = "CaptionButtonBackgroundPointerOver";
    private static readonly string s_SysCaptionForegroundHover = "CaptionButtonForegroundPointerOver";
    private static readonly string s_SysCaptionBackgroundPressed = "CaptionButtonBackgroundPressed";
    private static readonly string s_SysCaptionForegroundPressed = "CaptionButtonForegroundPressed";
    private static readonly string s_SysCaptionBackgroundInactive = "CaptionButtonBackgroundInactive";
    private static readonly string s_SysCaptionForegroundInactive = "CaptionButtonForegroundInactive";
}