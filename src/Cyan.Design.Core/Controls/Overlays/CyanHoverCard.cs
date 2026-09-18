using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Overlays;

/// <summary>悬停卡片浮层位置</summary>
public enum HoverCardPlacement
{
    /// <summary>顶部</summary>
    Top,
    /// <summary>底部</summary>
    Bottom,
    /// <summary>左侧</summary>
    Left,
    /// <summary>右侧</summary>
    Right,
    /// <summary>左上</summary>
    TopLeft,
    /// <summary>右上</summary>
    TopRight,
    /// <summary>左下</summary>
    BottomLeft,
    /// <summary>右下</summary>
    BottomRight
}

/// <summary>悬停卡片控件，鼠标悬停时展示浮层内容</summary>
public class CyanHoverCard : ContentControl
{
    /// <summary>浮层内容样式属性</summary>
    public static readonly StyledProperty<object?> OverlayProperty =
        AvaloniaProperty.Register<CyanHoverCard, object?>(nameof(Overlay));

    /// <summary>浮层位置样式属性</summary>
    public static readonly StyledProperty<HoverCardPlacement> PlacementProperty =
        AvaloniaProperty.Register<CyanHoverCard, HoverCardPlacement>(nameof(Placement), HoverCardPlacement.Top);

    /// <summary>是否显示箭头样式属性</summary>
    public static readonly StyledProperty<bool> ArrowProperty =
        AvaloniaProperty.Register<CyanHoverCard, bool>(nameof(Arrow), true);

    /// <summary>禁用状态样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanHoverCard, bool>(nameof(Disabled));

    /// <summary>展开状态样式属性</summary>
    public static readonly StyledProperty<bool> OpenProperty =
        AvaloniaProperty.Register<CyanHoverCard, bool>(nameof(Open), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    /// <summary>展开延迟样式属性</summary>
    public static readonly StyledProperty<int> OpenDelayProperty =
        AvaloniaProperty.Register<CyanHoverCard, int>(nameof(OpenDelay), 600);

    /// <summary>关闭延迟样式属性</summary>
    public static readonly StyledProperty<int> CloseDelayProperty =
        AvaloniaProperty.Register<CyanHoverCard, int>(nameof(CloseDelay), 300);

    /// <summary>浮层最大宽度样式属性</summary>
    public static readonly StyledProperty<double> OverlayMaxWidthProperty =
        AvaloniaProperty.Register<CyanHoverCard, double>(nameof(OverlayMaxWidth), 320);

    /// <summary>展开状态变化路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> OpenChangedEvent =
        RoutedEvent.Register<CyanHoverCard, RoutedEventArgs>(nameof(OpenChanged), RoutingStrategies.Bubble);

    private Popup? _popup;
    private bool _hoveringTrigger;
    private bool _hoveringContent;
    private DispatcherTimer? _openTimer;
    private DispatcherTimer? _closeTimer;

    static CyanHoverCard()
    {
        OpenProperty.Changed.AddClassHandler<CyanHoverCard>((c, _) => c.OnOpenChanged());
        DisabledProperty.Changed.AddClassHandler<CyanHoverCard>((c, _) => c.UpdatePseudoClasses());
    }

    /// <summary>浮层内容</summary>
    public object? Overlay
    {
        get => GetValue(OverlayProperty);
        set => SetValue(OverlayProperty, value);
    }

    /// <summary>浮层位置</summary>
    public HoverCardPlacement Placement
    {
        get => GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>是否显示箭头</summary>
    public bool Arrow
    {
        get => GetValue(ArrowProperty);
        set => SetValue(ArrowProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool Disabled
    {
        get => GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>是否展开</summary>
    public bool Open
    {
        get => GetValue(OpenProperty);
        set => SetValue(OpenProperty, value);
    }

    /// <summary>展开延迟（毫秒）</summary>
    public int OpenDelay
    {
        get => GetValue(OpenDelayProperty);
        set => SetValue(OpenDelayProperty, value);
    }

    /// <summary>关闭延迟（毫秒）</summary>
    public int CloseDelay
    {
        get => GetValue(CloseDelayProperty);
        set => SetValue(CloseDelayProperty, value);
    }

    /// <summary>浮层最大宽度</summary>
    public double OverlayMaxWidth
    {
        get => GetValue(OverlayMaxWidthProperty);
        set => SetValue(OverlayMaxWidthProperty, value);
    }

    /// <summary>展开状态变化事件</summary>
    public event EventHandler<RoutedEventArgs>? OpenChanged
    {
        add => AddHandler(OpenChangedEvent, value);
        remove => RemoveHandler(OpenChangedEvent, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (_popup is not null)
        {
            _popup.PointerEntered -= OnPopupPointerEntered;
            _popup.PointerExited -= OnPopupPointerExited;
        }

        _popup = e.NameScope.Find<Popup>("PART_Popup");
        if (_popup is not null)
        {
            _popup.PlacementTarget = this;
            _popup.Placement = GetPlacementMode(Placement);
            _popup.PointerEntered += OnPopupPointerEntered;
            _popup.PointerExited += OnPopupPointerExited;
        }
        UpdatePseudoClasses();
    }

    /// <summary>指针进入事件处理</summary>
    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        if (Disabled) return;
        _hoveringTrigger = true;
        CancelClose();
        ScheduleOpen();
    }

    /// <summary>指针离开事件处理</summary>
    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        _hoveringTrigger = false;
        ScheduleClose();
    }

    private void OnPopupPointerEntered(object? sender, PointerEventArgs e)
    {
        _hoveringContent = true;
        CancelClose();
    }

    private void OnPopupPointerExited(object? sender, PointerEventArgs e)
    {
        _hoveringContent = false;
        ScheduleClose();
    }

    private void ScheduleOpen()
    {
        _openTimer?.Stop();
        if (OpenDelay <= 0)
        {
            Open = true;
            return;
        }
        _openTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(OpenDelay), DispatcherPriority.Normal, (_, _) =>
        {
            if (_hoveringTrigger && !Disabled)
                Open = true;
            _openTimer?.Stop();
        });
        _openTimer.Start();
    }

    private void ScheduleClose()
    {
        _closeTimer?.Stop();
        if (CloseDelay <= 0)
        {
            TryClose();
            return;
        }
        _closeTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(CloseDelay), DispatcherPriority.Normal, (_, _) =>
        {
            TryClose();
            _closeTimer?.Stop();
        });
        _closeTimer.Start();
    }

    private void TryClose()
    {
        if (!_hoveringTrigger && !_hoveringContent)
            Open = false;
    }

    private void CancelClose() => _closeTimer?.Stop();

    private void OnOpenChanged()
    {
        PseudoClasses.Set(":open", Open);
        RaiseEvent(new RoutedEventArgs(OpenChangedEvent));
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":disabled", Disabled);
        PseudoClasses.Set(":open", Open);
    }

    /// <summary>从视觉树分离时处理</summary>
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _openTimer?.Stop();
        _closeTimer?.Stop();
        if (_popup is not null)
        {
            _popup.PointerEntered -= OnPopupPointerEntered;
            _popup.PointerExited -= OnPopupPointerExited;
        }
    }

    private static PlacementMode GetPlacementMode(HoverCardPlacement placement)
    {
        return placement switch
        {
            HoverCardPlacement.Top => PlacementMode.Top,
            HoverCardPlacement.Bottom => PlacementMode.Bottom,
            HoverCardPlacement.Left => PlacementMode.Left,
            HoverCardPlacement.Right => PlacementMode.Right,
            HoverCardPlacement.TopLeft => PlacementMode.TopEdgeAlignedLeft,
            HoverCardPlacement.TopRight => PlacementMode.TopEdgeAlignedRight,
            HoverCardPlacement.BottomLeft => PlacementMode.BottomEdgeAlignedLeft,
            HoverCardPlacement.BottomRight => PlacementMode.BottomEdgeAlignedRight,
            _ => PlacementMode.Top
        };
    }
}