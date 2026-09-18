using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Dropdowns;

/// <summary>下拉控件，将内容与浮层组合展示</summary>
public class CyanDropdown : ContentControl
{
    /// <summary>下拉浮层内容样式属性</summary>
    public static readonly StyledProperty<object?> OverlayProperty =
        AvaloniaProperty.Register<CyanDropdown, object?>(nameof(Overlay));

    /// <summary>下拉触发方式样式属性</summary>
    public static readonly StyledProperty<DropdownTrigger> TriggerProperty =
        AvaloniaProperty.Register<CyanDropdown, DropdownTrigger>(nameof(Trigger), DropdownTrigger.Hover);

    /// <summary>下拉浮层位置样式属性</summary>
    public static readonly StyledProperty<DropdownPlacement> PlacementProperty =
        AvaloniaProperty.Register<CyanDropdown, DropdownPlacement>(nameof(Placement), DropdownPlacement.BottomLeft);

    /// <summary>是否显示箭头样式属性</summary>
    public static readonly StyledProperty<bool> ArrowProperty =
        AvaloniaProperty.Register<CyanDropdown, bool>(nameof(Arrow));

    /// <summary>禁用状态样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanDropdown, bool>(nameof(Disabled));

    /// <summary>下拉打开状态样式属性</summary>
    public static readonly StyledProperty<bool> OpenProperty =
        AvaloniaProperty.Register<CyanDropdown, bool>(nameof(Open), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    /// <summary>打开状态变化路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> OpenChangedEvent =
        RoutedEvent.Register<CyanDropdown, RoutedEventArgs>(nameof(OpenChanged), RoutingStrategies.Bubble);

    private Popup? _popup;
    private bool _hovering;

    static CyanDropdown()
    {
        OpenProperty.Changed.AddClassHandler<CyanDropdown>((d, _) => d.OnOpenChanged());
        TriggerProperty.Changed.AddClassHandler<CyanDropdown>((d, _) => d.UpdatePseudoClasses());
        DisabledProperty.Changed.AddClassHandler<CyanDropdown>((d, _) => d.UpdatePseudoClasses());
        PlacementProperty.Changed.AddClassHandler<CyanDropdown>((d, _) => d.OnPlacementChanged());
    }

    /// <summary>初始化 CyanDropdown 的新实例</summary>
    public CyanDropdown()
    {
        AddHandler(PointerPressedEvent, OnPointerPressedCore, RoutingStrategies.Bubble, handledEventsToo: true);
    }

    /// <summary>下拉浮层内容</summary>
    public object? Overlay
    {
        get => GetValue(OverlayProperty);
        set => SetValue(OverlayProperty, value);
    }

    /// <summary>下拉触发方式</summary>
    public DropdownTrigger Trigger
    {
        get => GetValue(TriggerProperty);
        set => SetValue(TriggerProperty, value);
    }

    /// <summary>下拉浮层位置</summary>
    public DropdownPlacement Placement
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

    /// <summary>下拉是否打开</summary>
    public bool Open
    {
        get => GetValue(OpenProperty);
        set => SetValue(OpenProperty, value);
    }

    /// <summary>打开状态变化事件</summary>
    public event EventHandler<RoutedEventArgs>? OpenChanged
    {
        add => AddHandler(OpenChangedEvent, value);
        remove => RemoveHandler(OpenChangedEvent, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        DropdownHoverHandler.DetachPopupChildHandlers(_popup, OnPopupChildPointerChanged);
        _popup = e.NameScope.Find<Popup>("PART_Popup");
        if (_popup != null)
        {
            _popup.PlacementTarget = this;
            _popup.Placement = GetPlacementMode(Placement);
            _popup.IsLightDismissEnabled = Trigger != DropdownTrigger.Hover;
            DropdownHoverHandler.AttachPopupChildHandlers(_popup, OnPopupChildPointerChanged);
        }
        UpdatePseudoClasses();
    }


    private void OnPlacementChanged()
    {
        if (_popup != null)
            _popup.Placement = GetPlacementMode(Placement);
    }

    /// <summary>指针进入事件处理</summary>
    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        if (Disabled) return;
        _hovering = true;
        if (Trigger == DropdownTrigger.Hover)
            Open = true;
    }

    /// <summary>指针离开事件处理</summary>
    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        _hovering = false;
        if (Trigger == DropdownTrigger.Hover)
            ScheduleHoverClose();
    }

    private void OnPopupChildPointerChanged(object? sender, PointerEventArgs e)
    {
        if (Trigger != DropdownTrigger.Hover) return;
        ScheduleHoverClose();
    }

    private void ScheduleHoverClose()
    {
        if (Trigger != DropdownTrigger.Hover) return;
        DropdownHoverHandler.ScheduleHoverClose(_popup, _hovering, () => Open = false);
    }

    private void OnPointerPressedCore(object? sender, PointerPressedEventArgs e)
    {
        if (Disabled) return;
        if (Trigger == DropdownTrigger.Click)
        {
            Open = !Open;
            e.Handled = true;
        }
        else if (Trigger == DropdownTrigger.ContextMenu && e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.RightButtonPressed)
        {
            Open = true;
            e.Handled = true;
        }
    }

    private void OnOpenChanged()
    {
        PseudoClasses.Set(":open", Open);
        RaiseEvent(new RoutedEventArgs(OpenChangedEvent));
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":disabled", Disabled);
        PseudoClasses.Set(":open", Open);
        if (_popup != null)
            _popup.IsLightDismissEnabled = Trigger != DropdownTrigger.Hover;
    }

    /// <summary>从视觉树分离时处理</summary>
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        DropdownHoverHandler.DetachPopupChildHandlers(_popup, OnPopupChildPointerChanged);
    }

    private static PlacementMode GetPlacementMode(DropdownPlacement placement)
    {
        return placement switch
        {
            DropdownPlacement.BottomLeft => PlacementMode.BottomEdgeAlignedLeft,
            DropdownPlacement.Bottom => PlacementMode.Bottom,
            DropdownPlacement.BottomRight => PlacementMode.BottomEdgeAlignedRight,
            DropdownPlacement.TopLeft => PlacementMode.TopEdgeAlignedLeft,
            DropdownPlacement.Top => PlacementMode.Top,
            DropdownPlacement.TopRight => PlacementMode.TopEdgeAlignedRight,
            DropdownPlacement.Left => PlacementMode.Left,
            DropdownPlacement.LeftTop => PlacementMode.LeftEdgeAlignedTop,
            DropdownPlacement.LeftBottom => PlacementMode.LeftEdgeAlignedBottom,
            DropdownPlacement.Right => PlacementMode.Right,
            DropdownPlacement.RightTop => PlacementMode.RightEdgeAlignedTop,
            DropdownPlacement.RightBottom => PlacementMode.RightEdgeAlignedBottom,
            _ => PlacementMode.BottomEdgeAlignedLeft
        };
    }
}
