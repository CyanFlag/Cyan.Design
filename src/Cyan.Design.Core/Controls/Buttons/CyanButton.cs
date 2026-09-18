using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Buttons;

/// <summary>按钮控件，支持多种类型、尺寸、形状及加载状态</summary>
public class CyanButton : Button
{
    /// <summary>按钮类型样式属性</summary>
    public static readonly StyledProperty<ButtonType> ButtonTypeProperty =
        AvaloniaProperty.Register<CyanButton, ButtonType>(nameof(ButtonType), ButtonType.Default);

    /// <summary>按钮尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> ButtonSizeProperty =
        AvaloniaProperty.Register<CyanButton, ControlSize>(nameof(ButtonSize), ControlSize.Middle);

    /// <summary>危险按钮样式属性</summary>
    public static readonly StyledProperty<bool> DangerProperty =
        AvaloniaProperty.Register<CyanButton, bool>(nameof(Danger));

    /// <summary>按钮形状样式属性</summary>
    public static readonly StyledProperty<ButtonShape> ShapeProperty =
        AvaloniaProperty.Register<CyanButton, ButtonShape>(nameof(Shape), ButtonShape.Default);

    /// <summary>块状按钮样式属性</summary>
    public static readonly StyledProperty<bool> BlockProperty =
        AvaloniaProperty.Register<CyanButton, bool>(nameof(Block));

    /// <summary>加载状态样式属性</summary>
    public static readonly StyledProperty<bool> LoadingProperty =
        AvaloniaProperty.Register<CyanButton, bool>(nameof(Loading));

    /// <summary>幽灵按钮样式属性</summary>
    public static readonly StyledProperty<bool> GhostProperty =
        AvaloniaProperty.Register<CyanButton, bool>(nameof(Ghost));

    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanButton, object?>(nameof(Icon));

    /// <summary>按钮阴影样式属性</summary>
    public static readonly StyledProperty<BoxShadows> BoxShadowProperty =
        AvaloniaProperty.Register<CyanButton, BoxShadows>(nameof(BoxShadow));

    private RotateTransform? _loadingRotate;
    private DispatcherTimer? _loadingTimer;
    private ContentPresenter? _iconPresenter;
    private ContentPresenter? _contentPresenter;

    static CyanButton()
    {
        LoadingProperty.Changed.AddClassHandler<CyanButton>(OnLoadingChanged);
        IconProperty.Changed.AddClassHandler<CyanButton>(OnIconChanged);
        ContentProperty.Changed.AddClassHandler<CyanButton>(OnContentChanged);
    }

    /// <summary>按钮类型，决定按钮的视觉风格</summary>
    public ButtonType ButtonType
    {
        get => GetValue(ButtonTypeProperty);
        set => SetValue(ButtonTypeProperty, value);
    }

    /// <summary>按钮尺寸</summary>
    public ControlSize ButtonSize
    {
        get => GetValue(ButtonSizeProperty);
        set => SetValue(ButtonSizeProperty, value);
    }

    /// <summary>是否为危险按钮</summary>
    public bool Danger
    {
        get => GetValue(DangerProperty);
        set => SetValue(DangerProperty, value);
    }

    /// <summary>按钮形状</summary>
    public ButtonShape Shape
    {
        get => GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    /// <summary>是否为块状按钮，撑满父容器宽度</summary>
    public bool Block
    {
        get => GetValue(BlockProperty);
        set => SetValue(BlockProperty, value);
    }

    /// <summary>是否处于加载状态</summary>
    public bool Loading
    {
        get => GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    /// <summary>是否为幽灵按钮，背景透明</summary>
    public bool Ghost
    {
        get => GetValue(GhostProperty);
        set => SetValue(GhostProperty, value);
    }

    /// <summary>按钮图标内容</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>按钮阴影</summary>
    public BoxShadows BoxShadow
    {
        get => GetValue(BoxShadowProperty);
        set => SetValue(BoxShadowProperty, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (e.NameScope.Find<Avalonia.Controls.Shapes.Path>("LoadingIcon") is { } path)
            _loadingRotate = path.RenderTransform as RotateTransform;
        _iconPresenter = e.NameScope.Find<ContentPresenter>("PART_IconPresenter");
        _contentPresenter = e.NameScope.Find<ContentPresenter>("PART_ContentPresenter");
        UpdateLoadingTimer();
        UpdateIconVisibility();
        UpdateContentVisibility();
    }

    /// <summary>点击事件处理</summary>
    protected override void OnClick()
    {
        if (!Loading)
            base.OnClick();
    }

    private static void OnLoadingChanged(CyanButton c, AvaloniaPropertyChangedEventArgs e)
    {
        c.UpdateLoadingTimer();
        c.UpdateIconVisibility();
    }

    private static void OnIconChanged(CyanButton c, AvaloniaPropertyChangedEventArgs e)
        => c.UpdateIconVisibility();

    private static void OnContentChanged(CyanButton c, AvaloniaPropertyChangedEventArgs e)
        => c.UpdateContentVisibility();

    private void UpdateIconVisibility()
    {
        if (_iconPresenter != null)
            _iconPresenter.IsVisible = Icon != null && !Loading;
    }

    private void UpdateContentVisibility()
    {
        if (_contentPresenter != null)
            _contentPresenter.IsVisible = Content != null;
    }

    private void UpdateLoadingTimer()
    {
        if (Loading && _loadingRotate != null)
        {
            _loadingTimer ??= new DispatcherTimer(TimeSpan.FromMilliseconds(16), DispatcherPriority.Render, OnLoadingTick);
            _loadingTimer.Start();
        }
        else
        {
            _loadingTimer?.Stop();
        }
    }

    private void OnLoadingTick(object? sender, System.EventArgs e)
    {
        if (_loadingRotate != null)
            _loadingRotate.Angle = (_loadingRotate.Angle + 6) % 360;
    }
}
