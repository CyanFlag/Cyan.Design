using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Buttons;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Dropdowns;

/// <summary>下拉按钮控件，结合按钮与下拉浮层使用</summary>
public class CyanDropdownButton : ContentControl
{
    /// <summary>下拉浮层内容样式属性</summary>
    public static readonly StyledProperty<object?> OverlayProperty =
        AvaloniaProperty.Register<CyanDropdownButton, object?>(nameof(Overlay));

    /// <summary>按钮类型样式属性</summary>
    public static readonly StyledProperty<ButtonType> ButtonTypeProperty =
        AvaloniaProperty.Register<CyanDropdownButton, ButtonType>(nameof(ButtonType), ButtonType.Default);

    /// <summary>按钮尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> ButtonSizeProperty =
        AvaloniaProperty.Register<CyanDropdownButton, ControlSize>(nameof(ButtonSize), ControlSize.Middle);

    /// <summary>危险状态样式属性</summary>
    public static readonly StyledProperty<bool> DangerProperty =
        AvaloniaProperty.Register<CyanDropdownButton, bool>(nameof(Danger));

    /// <summary>禁用状态样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanDropdownButton, bool>(nameof(Disabled));

    /// <summary>加载中状态样式属性</summary>
    public static readonly StyledProperty<bool> LoadingProperty =
        AvaloniaProperty.Register<CyanDropdownButton, bool>(nameof(Loading));

    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanDropdownButton, object?>(nameof(Icon));

    /// <summary>下拉浮层位置样式属性</summary>
    public static readonly StyledProperty<DropdownPlacement> PlacementProperty =
        AvaloniaProperty.Register<CyanDropdownButton, DropdownPlacement>(nameof(Placement), DropdownPlacement.BottomRight);

    /// <summary>下拉触发方式样式属性</summary>
    public static readonly StyledProperty<DropdownTrigger> TriggerProperty =
        AvaloniaProperty.Register<CyanDropdownButton, DropdownTrigger>(nameof(Trigger), DropdownTrigger.Click);

    /// <summary>下拉打开状态样式属性</summary>
    public static readonly StyledProperty<bool> OpenProperty =
        AvaloniaProperty.Register<CyanDropdownButton, bool>(nameof(Open), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    /// <summary>点击路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
        RoutedEvent.Register<CyanDropdownButton, RoutedEventArgs>(nameof(Click), RoutingStrategies.Bubble);

    private CyanButton? _leftButton;

    static CyanDropdownButton()
    {
        ButtonTypeProperty.Changed.AddClassHandler<CyanDropdownButton>((d, _) => d.UpdatePseudoClasses());
        DangerProperty.Changed.AddClassHandler<CyanDropdownButton>((d, _) => d.UpdatePseudoClasses());
        DisabledProperty.Changed.AddClassHandler<CyanDropdownButton>((d, _) => d.OnDisabledChanged());
    }

    /// <summary>下拉浮层内容</summary>
    public object? Overlay
    {
        get => GetValue(OverlayProperty);
        set => SetValue(OverlayProperty, value);
    }

    /// <summary>按钮类型</summary>
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

    /// <summary>是否处于危险状态</summary>
    public bool Danger
    {
        get => GetValue(DangerProperty);
        set => SetValue(DangerProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool Disabled
    {
        get => GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>是否处于加载中状态</summary>
    public bool Loading
    {
        get => GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    /// <summary>图标内容</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>下拉浮层位置</summary>
    public DropdownPlacement Placement
    {
        get => GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>下拉触发方式</summary>
    public DropdownTrigger Trigger
    {
        get => GetValue(TriggerProperty);
        set => SetValue(TriggerProperty, value);
    }

    /// <summary>下拉是否打开</summary>
    public bool Open
    {
        get => GetValue(OpenProperty);
        set => SetValue(OpenProperty, value);
    }

    /// <summary>点击事件</summary>
    public event EventHandler<RoutedEventArgs>? Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (_leftButton != null)
            _leftButton.Click -= OnLeftButtonClick;
        _leftButton = e.NameScope.Find<CyanButton>("PART_LeftButton");
        if (_leftButton != null)
        {
            _leftButton.Click += OnLeftButtonClick;
            _leftButton.IsEnabled = !Disabled;
        }
        UpdatePseudoClasses();
    }

    private void OnLeftButtonClick(object? sender, RoutedEventArgs e)
        => RaiseEvent(new RoutedEventArgs(ClickEvent));

    private void OnDisabledChanged()
    {
        if (_leftButton != null)
            _leftButton.IsEnabled = !Disabled;
        UpdatePseudoClasses();
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":primary", ButtonType == ButtonType.Primary);
        PseudoClasses.Set(":dashed", ButtonType == ButtonType.Dashed);
        PseudoClasses.Set(":danger", Danger);
        PseudoClasses.Set(":disabled", Disabled);
    }
}
