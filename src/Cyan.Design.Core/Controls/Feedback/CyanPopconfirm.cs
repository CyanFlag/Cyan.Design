using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Cyan.Design.Core.Controls.Buttons;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>气泡确认框控件，用于在执行危险操作前进行二次确认</summary>
public class CyanPopconfirm : ContentControl
{
    /// <summary>是否打开确认框的样式属性</summary>
    public static readonly StyledProperty<bool> OpenProperty =
        AvaloniaProperty.Register<CyanPopconfirm, bool>(nameof(Open), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    /// <summary>确认框标题的样式属性</summary>
    public static readonly StyledProperty<object?> TitleProperty =
        AvaloniaProperty.Register<CyanPopconfirm, object?>(nameof(Title));

    /// <summary>确认框描述内容的样式属性</summary>
    public static readonly StyledProperty<object?> DescriptionProperty =
        AvaloniaProperty.Register<CyanPopconfirm, object?>(nameof(Description));

    /// <summary>确认按钮文本的样式属性</summary>
    public static readonly StyledProperty<string> OkTextProperty =
        AvaloniaProperty.Register<CyanPopconfirm, string>(nameof(OkText), "确定");

    /// <summary>取消按钮文本的样式属性</summary>
    public static readonly StyledProperty<string> CancelTextProperty =
        AvaloniaProperty.Register<CyanPopconfirm, string>(nameof(CancelText), "取消");

    /// <summary>确认按钮类型的样式属性</summary>
    public static readonly StyledProperty<ButtonType> OkTypeProperty =
        AvaloniaProperty.Register<CyanPopconfirm, ButtonType>(nameof(OkType), ButtonType.Primary);

    /// <summary>是否显示取消按钮的样式属性</summary>
    public static readonly StyledProperty<bool> ShowCancelProperty =
        AvaloniaProperty.Register<CyanPopconfirm, bool>(nameof(ShowCancel), true);

    /// <summary>确认框图标的样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanPopconfirm, object?>(nameof(Icon));

    /// <summary>是否禁用的样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanPopconfirm, bool>(nameof(Disabled));

    /// <summary>确认框弹出位置的样式属性</summary>
    public static readonly StyledProperty<PopconfirmPlacement> PlacementProperty =
        AvaloniaProperty.Register<CyanPopconfirm, PopconfirmPlacement>(nameof(Placement), PopconfirmPlacement.Top);

    /// <summary>是否显示箭头的样式属性</summary>
    public static readonly StyledProperty<bool> ArrowProperty =
        AvaloniaProperty.Register<CyanPopconfirm, bool>(nameof(Arrow), true);

    /// <summary>确认按钮是否处于加载状态的样式属性</summary>
    public static readonly StyledProperty<bool> ConfirmLoadingProperty =
        AvaloniaProperty.Register<CyanPopconfirm, bool>(nameof(ConfirmLoading));

    /// <summary>触发方式的样式属性</summary>
    public static readonly StyledProperty<PopconfirmTrigger> TriggerProperty =
        AvaloniaProperty.Register<CyanPopconfirm, PopconfirmTrigger>(nameof(Trigger), PopconfirmTrigger.Click);

    /// <summary>确认按钮点击事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ConfirmEvent =
        RoutedEvent.Register<CyanPopconfirm, RoutedEventArgs>(nameof(Confirm), RoutingStrategies.Bubble);

    /// <summary>取消按钮点击事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> CancelEvent =
        RoutedEvent.Register<CyanPopconfirm, RoutedEventArgs>(nameof(Cancel), RoutingStrategies.Bubble);

    static CyanPopconfirm()
    {
        OpenProperty.Changed.AddClassHandler<CyanPopconfirm>((p, _) => p.OnOpenChanged());
        DisabledProperty.Changed.AddClassHandler<CyanPopconfirm>((p, _) => p.UpdatePseudoClasses());
        PlacementProperty.Changed.AddClassHandler<CyanPopconfirm>((p, _) => p.OnPlacementChanged());
        TitleProperty.Changed.AddClassHandler<CyanPopconfirm>((p, _) => p.UpdatePseudoClasses());
        DescriptionProperty.Changed.AddClassHandler<CyanPopconfirm>((p, _) => p.UpdatePseudoClasses());
        IconProperty.Changed.AddClassHandler<CyanPopconfirm>((p, _) => p.UpdatePseudoClasses());
        ShowCancelProperty.Changed.AddClassHandler<CyanPopconfirm>((p, _) => p.UpdatePseudoClasses());
        ConfirmLoadingProperty.Changed.AddClassHandler<CyanPopconfirm>((p, _) => p.UpdatePseudoClasses());
        TriggerProperty.Changed.AddClassHandler<CyanPopconfirm>((p, _) => p.UpdatePseudoClasses());
    }

    /// <summary>初始化控件实例</summary>
    public CyanPopconfirm()
    {
        AddHandler(PointerPressedEvent, OnPointerPressedCore, RoutingStrategies.Bubble, handledEventsToo: true);
    }

    /// <summary>是否打开确认框</summary>
    public bool Open
    {
        get => GetValue(OpenProperty);
        set => SetValue(OpenProperty, value);
    }

    /// <summary>确认框标题</summary>
    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>确认框描述内容</summary>
    public object? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>确认按钮文本</summary>
    public string OkText
    {
        get => GetValue(OkTextProperty);
        set => SetValue(OkTextProperty, value);
    }

    /// <summary>取消按钮文本</summary>
    public string CancelText
    {
        get => GetValue(CancelTextProperty);
        set => SetValue(CancelTextProperty, value);
    }

    /// <summary>确认按钮类型</summary>
    public ButtonType OkType
    {
        get => GetValue(OkTypeProperty);
        set => SetValue(OkTypeProperty, value);
    }

    /// <summary>是否显示取消按钮</summary>
    public bool ShowCancel
    {
        get => GetValue(ShowCancelProperty);
        set => SetValue(ShowCancelProperty, value);
    }

    /// <summary>确认框图标</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool Disabled
    {
        get => GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>确认框弹出位置</summary>
    public PopconfirmPlacement Placement
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

    /// <summary>确认按钮是否处于加载状态</summary>
    public bool ConfirmLoading
    {
        get => GetValue(ConfirmLoadingProperty);
        set => SetValue(ConfirmLoadingProperty, value);
    }

    /// <summary>触发方式</summary>
    public PopconfirmTrigger Trigger
    {
        get => GetValue(TriggerProperty);
        set => SetValue(TriggerProperty, value);
    }

    /// <summary>确认按钮点击事件</summary>
    public event EventHandler<RoutedEventArgs>? Confirm
    {
        add => AddHandler(ConfirmEvent, value);
        remove => RemoveHandler(ConfirmEvent, value);
    }

    /// <summary>取消按钮点击事件</summary>
    public event EventHandler<RoutedEventArgs>? Cancel
    {
        add => AddHandler(CancelEvent, value);
        remove => RemoveHandler(CancelEvent, value);
    }

    private Popup? _popup;
    private bool _hovering;

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
            _popup.IsLightDismissEnabled = Trigger != PopconfirmTrigger.Hover;
            DropdownHoverHandler.AttachPopupChildHandlers(_popup, OnPopupChildPointerChanged);
        }

        var okButton = e.NameScope.Find<Button>("PART_OkButton");
        if (okButton != null)
            okButton.Click += OnOkButtonClick;

        var cancelButton = e.NameScope.Find<Button>("PART_CancelButton");
        if (cancelButton != null)
            cancelButton.Click += OnCancelButtonClick;

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
        if (Trigger == PopconfirmTrigger.Hover)
            Open = true;
    }

    /// <summary>指针离开事件处理</summary>
    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        _hovering = false;
        if (Trigger == PopconfirmTrigger.Hover)
            ScheduleHoverClose();
    }

    private void OnPopupChildPointerChanged(object? sender, PointerEventArgs e)
    {
        if (Trigger != PopconfirmTrigger.Hover) return;
        ScheduleHoverClose();
    }

    private void ScheduleHoverClose()
    {
        if (Trigger != PopconfirmTrigger.Hover) return;
        DropdownHoverHandler.ScheduleHoverClose(_popup, _hovering, () => Open = false);
    }

    private void OnPointerPressedCore(object? sender, PointerPressedEventArgs e)
    {
        if (Disabled) return;
        if (Trigger == PopconfirmTrigger.Click)
        {
            Open = !Open;
            e.Handled = true;
        }
    }

    private void OnOkButtonClick(object? sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(ConfirmEvent));
    }

    private void OnCancelButtonClick(object? sender, RoutedEventArgs e)
    {
        Open = false;
        RaiseEvent(new RoutedEventArgs(CancelEvent));
    }

    private void OnOpenChanged()
    {
        PseudoClasses.Set(":open", Open);
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":open", Open);
        PseudoClasses.Set(":disabled", Disabled);
        PseudoClasses.Set(":has-icon", Icon != null);
        PseudoClasses.Set(":has-description", Description != null);
        PseudoClasses.Set(":no-cancel", !ShowCancel);
        PseudoClasses.Set(":confirm-loading", ConfirmLoading);
        PseudoClasses.Set(":top", Placement is PopconfirmPlacement.Top or PopconfirmPlacement.TopLeft or PopconfirmPlacement.TopRight);
        PseudoClasses.Set(":bottom", Placement is PopconfirmPlacement.Bottom or PopconfirmPlacement.BottomLeft or PopconfirmPlacement.BottomRight);
        PseudoClasses.Set(":left", Placement is PopconfirmPlacement.Left or PopconfirmPlacement.LeftTop or PopconfirmPlacement.LeftBottom);
        PseudoClasses.Set(":right", Placement is PopconfirmPlacement.Right or PopconfirmPlacement.RightTop or PopconfirmPlacement.RightBottom);
        if (_popup != null)
            _popup.IsLightDismissEnabled = Trigger != PopconfirmTrigger.Hover;
    }

    /// <summary>从视觉树分离时处理</summary>
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        DropdownHoverHandler.DetachPopupChildHandlers(_popup, OnPopupChildPointerChanged);
    }

    private static PlacementMode GetPlacementMode(PopconfirmPlacement placement)
    {
        return placement switch
        {
            PopconfirmPlacement.Top => PlacementMode.Top,
            PopconfirmPlacement.TopLeft => PlacementMode.TopEdgeAlignedLeft,
            PopconfirmPlacement.TopRight => PlacementMode.TopEdgeAlignedRight,
            PopconfirmPlacement.Bottom => PlacementMode.Bottom,
            PopconfirmPlacement.BottomLeft => PlacementMode.BottomEdgeAlignedLeft,
            PopconfirmPlacement.BottomRight => PlacementMode.BottomEdgeAlignedRight,
            PopconfirmPlacement.Left => PlacementMode.Left,
            PopconfirmPlacement.LeftTop => PlacementMode.LeftEdgeAlignedTop,
            PopconfirmPlacement.LeftBottom => PlacementMode.LeftEdgeAlignedBottom,
            PopconfirmPlacement.Right => PlacementMode.Right,
            PopconfirmPlacement.RightTop => PlacementMode.RightEdgeAlignedTop,
            PopconfirmPlacement.RightBottom => PlacementMode.RightEdgeAlignedBottom,
            _ => PlacementMode.Top
        };
    }
}
