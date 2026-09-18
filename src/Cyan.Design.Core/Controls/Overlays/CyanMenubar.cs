using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Overlays;

/// <summary>菜单栏容器控件</summary>
public class CyanMenubar : Border
{
}

/// <summary>菜单栏菜单控件，点击展开浮层</summary>
public class CyanMenubarMenu : HeaderedContentControl
{
    /// <summary>是否展开样式属性</summary>
    public static readonly StyledProperty<bool> IsOpenProperty =
        AvaloniaProperty.Register<CyanMenubarMenu, bool>(nameof(IsOpen));

    private Popup? _popup;

    static CyanMenubarMenu()
    {
        IsOpenProperty.Changed.AddClassHandler<CyanMenubarMenu>((m, _) => m.OnOpenChanged());
    }

    /// <summary>菜单是否展开</summary>
    public bool IsOpen
    {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _popup = e.NameScope.Find<Popup>("PART_Popup");
        if (_popup is not null)
        {
            _popup.PlacementTarget = this;
            _popup.Placement = PlacementMode.BottomEdgeAlignedLeft;
            _popup.IsLightDismissEnabled = true;
        }
    }

    /// <summary>指针进入事件处理</summary>
    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        var ancestor = this.GetVisualParent();
        while (ancestor is not null)
        {
            if (ancestor is CyanMenubar bar)
            {
                foreach (var child in bar.GetVisualDescendants())
                {
                    if (child is CyanMenubarMenu menu && menu != this && menu.IsOpen)
                    {
                        menu.IsOpen = false;
                        IsOpen = true;
                        return;
                    }
                }
                break;
            }
            ancestor = ancestor.GetVisualParent();
        }
    }

    /// <summary>指针按下事件处理</summary>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        IsOpen = !IsOpen;
        e.Handled = true;
    }

    private void OnOpenChanged()
    {
        PseudoClasses.Set(":open", IsOpen);
    }
}

/// <summary>菜单栏菜单项控件</summary>
public class CyanMenubarItem : HeaderedContentControl
{
    /// <summary>快捷键样式属性</summary>
    public static readonly StyledProperty<string> ShortcutProperty =
        AvaloniaProperty.Register<CyanMenubarItem, string>(nameof(Shortcut));

    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanMenubarItem, object?>(nameof(Icon));

    /// <summary>是否禁用样式属性</summary>
    public static readonly StyledProperty<bool> IsDisabledProperty =
        AvaloniaProperty.Register<CyanMenubarItem, bool>(nameof(IsDisabled));

    /// <summary>点击路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ClickedEvent =
        RoutedEvent.Register<CyanMenubarItem, RoutedEventArgs>(nameof(Clicked), RoutingStrategies.Bubble);

    static CyanMenubarItem()
    {
        IsDisabledProperty.Changed.AddClassHandler<CyanMenubarItem>((i, _) => i.UpdatePseudoClasses());
    }

    /// <summary>快捷键文本</summary>
    public string Shortcut
    {
        get => GetValue(ShortcutProperty);
        set => SetValue(ShortcutProperty, value);
    }

    /// <summary>菜单项图标</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool IsDisabled
    {
        get => GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    /// <summary>点击事件</summary>
    public event EventHandler<RoutedEventArgs>? Clicked
    {
        add => AddHandler(ClickedEvent, value);
        remove => RemoveHandler(ClickedEvent, value);
    }

    /// <summary>指针按下事件处理</summary>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (IsDisabled) return;

        RaiseEvent(new RoutedEventArgs(ClickedEvent));
        CloseParentMenu();
        e.Handled = true;
    }

    internal void CloseParentMenu()
    {
        var parent = this.Parent;
        while (parent is not null)
        {
            if (parent is CyanMenubarMenu menu)
            {
                menu.IsOpen = false;
                return;
            }
            if (parent is CyanMenubarSub sub)
            {
                sub.CloseAllMenus();
                return;
            }
            parent = parent.Parent;
        }
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":disabled", IsDisabled);
    }
}

/// <summary>菜单栏复选菜单项控件</summary>
public class CyanMenubarCheckboxItem : HeaderedContentControl
{
    /// <summary>是否选中样式属性</summary>
    public static readonly StyledProperty<bool> IsCheckedProperty =
        AvaloniaProperty.Register<CyanMenubarCheckboxItem, bool>(nameof(IsChecked));

    /// <summary>快捷键样式属性</summary>
    public static readonly StyledProperty<string> ShortcutProperty =
        AvaloniaProperty.Register<CyanMenubarCheckboxItem, string>(nameof(Shortcut));

    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanMenubarCheckboxItem, object?>(nameof(Icon));

    /// <summary>是否禁用样式属性</summary>
    public static readonly StyledProperty<bool> IsDisabledProperty =
        AvaloniaProperty.Register<CyanMenubarCheckboxItem, bool>(nameof(IsDisabled));

    /// <summary>点击路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ClickedEvent =
        RoutedEvent.Register<CyanMenubarCheckboxItem, RoutedEventArgs>(nameof(Clicked), RoutingStrategies.Bubble);

    static CyanMenubarCheckboxItem()
    {
        IsCheckedProperty.Changed.AddClassHandler<CyanMenubarCheckboxItem>((i, _) => i.UpdatePseudoClasses());
        IsDisabledProperty.Changed.AddClassHandler<CyanMenubarCheckboxItem>((i, _) => i.UpdatePseudoClasses());
    }

    /// <summary>是否选中</summary>
    public bool IsChecked
    {
        get => GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    /// <summary>快捷键文本</summary>
    public string Shortcut
    {
        get => GetValue(ShortcutProperty);
        set => SetValue(ShortcutProperty, value);
    }

    /// <summary>图标内容</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool IsDisabled
    {
        get => GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    /// <summary>点击事件</summary>
    public event EventHandler<RoutedEventArgs>? Clicked
    {
        add => AddHandler(ClickedEvent, value);
        remove => RemoveHandler(ClickedEvent, value);
    }

    /// <summary>指针按下事件处理</summary>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (IsDisabled) return;

        IsChecked = !IsChecked;
        RaiseEvent(new RoutedEventArgs(ClickedEvent));
        CloseParentMenu();
        e.Handled = true;
    }

    private void CloseParentMenu()
    {
        var parent = this.Parent;
        while (parent is not null)
        {
            if (parent is CyanMenubarMenu menu)
            {
                menu.IsOpen = false;
                return;
            }
            if (parent is CyanMenubarSub sub)
            {
                sub.CloseAllMenus();
                return;
            }
            parent = parent.Parent;
        }
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":checked", IsChecked);
        PseudoClasses.Set(":disabled", IsDisabled);
    }
}

/// <summary>菜单栏单选组控件，管理一组单选项</summary>
public class CyanMenubarRadioGroup : StackPanel
{
    /// <summary>选中值样式属性</summary>
    public static readonly StyledProperty<string> SelectedValueProperty =
        AvaloniaProperty.Register<CyanMenubarRadioGroup, string>(nameof(SelectedValue));

    /// <summary>当前选中值</summary>
    public string SelectedValue
    {
        get => GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }

    /// <summary>初始化 CyanMenubarRadioGroup 的新实例</summary>
    public CyanMenubarRadioGroup()
    {
        Orientation = Avalonia.Layout.Orientation.Vertical;
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        foreach (var child in Children)
        {
            if (child is CyanMenubarRadioItem radio)
            {
                radio.Clicked += OnRadioItemClicked;
                if (radio.Value == SelectedValue)
                    radio.IsSelected = true;
            }
        }
    }

    private void OnRadioItemClicked(object? sender, RoutedEventArgs e)
    {
        if (sender is not CyanMenubarRadioItem clicked) return;
        SelectedValue = clicked.Value;
        foreach (var child in Children)
        {
            if (child is CyanMenubarRadioItem radio)
                radio.IsSelected = radio == clicked;
        }
    }
}

/// <summary>菜单栏单选菜单项控件</summary>
public class CyanMenubarRadioItem : HeaderedContentControl
{
    /// <summary>选项值样式属性</summary>
    public static readonly StyledProperty<string> ValueProperty =
        AvaloniaProperty.Register<CyanMenubarRadioItem, string>(nameof(Value));

    /// <summary>是否选中样式属性</summary>
    public static readonly StyledProperty<bool> IsSelectedProperty =
        AvaloniaProperty.Register<CyanMenubarRadioItem, bool>(nameof(IsSelected));

    /// <summary>快捷键样式属性</summary>
    public static readonly StyledProperty<string> ShortcutProperty =
        AvaloniaProperty.Register<CyanMenubarRadioItem, string>(nameof(Shortcut));

    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanMenubarRadioItem, object?>(nameof(Icon));

    /// <summary>是否禁用样式属性</summary>
    public static readonly StyledProperty<bool> IsDisabledProperty =
        AvaloniaProperty.Register<CyanMenubarRadioItem, bool>(nameof(IsDisabled));

    /// <summary>点击路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ClickedEvent =
        RoutedEvent.Register<CyanMenubarRadioItem, RoutedEventArgs>(nameof(Clicked), RoutingStrategies.Bubble);

    static CyanMenubarRadioItem()
    {
        IsSelectedProperty.Changed.AddClassHandler<CyanMenubarRadioItem>((i, _) => i.UpdatePseudoClasses());
        IsDisabledProperty.Changed.AddClassHandler<CyanMenubarRadioItem>((i, _) => i.UpdatePseudoClasses());
    }

    /// <summary>选项值</summary>
    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>是否处于选中状态</summary>
    public bool IsSelected
    {
        get => GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    /// <summary>快捷键文本</summary>
    public string Shortcut
    {
        get => GetValue(ShortcutProperty);
        set => SetValue(ShortcutProperty, value);
    }

    /// <summary>图标内容</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool IsDisabled
    {
        get => GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    /// <summary>点击事件</summary>
    public event EventHandler<RoutedEventArgs>? Clicked
    {
        add => AddHandler(ClickedEvent, value);
        remove => RemoveHandler(ClickedEvent, value);
    }

    /// <summary>指针按下事件处理</summary>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (IsDisabled) return;

        IsSelected = true;
        RaiseEvent(new RoutedEventArgs(ClickedEvent));
        CloseParentMenu();
        e.Handled = true;
    }

    private void CloseParentMenu()
    {
        var parent = this.Parent;
        while (parent is not null)
        {
            if (parent is CyanMenubarMenu menu)
            {
                menu.IsOpen = false;
                return;
            }
            if (parent is CyanMenubarSub sub)
            {
                sub.CloseAllMenus();
                return;
            }
            parent = parent.Parent;
        }
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":selected", IsSelected);
        PseudoClasses.Set(":disabled", IsDisabled);
    }
}

/// <summary>菜单栏子菜单控件，悬停展开子菜单浮层</summary>
public class CyanMenubarSub : HeaderedContentControl
{
    /// <summary>是否展开样式属性</summary>
    public static readonly StyledProperty<bool> IsOpenProperty =
        AvaloniaProperty.Register<CyanMenubarSub, bool>(nameof(IsOpen));

    private Popup? _popup;

    static CyanMenubarSub()
    {
        IsOpenProperty.Changed.AddClassHandler<CyanMenubarSub>((s, _) => s.OnOpenChanged());
    }

    /// <summary>子菜单是否展开</summary>
    public bool IsOpen
    {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _popup = e.NameScope.Find<Popup>("PART_SubPopup");
        if (_popup is not null)
        {
            _popup.PlacementTarget = this;
            _popup.Placement = PlacementMode.RightEdgeAlignedTop;
            _popup.IsLightDismissEnabled = false;
        }
    }

    /// <summary>指针进入事件处理</summary>
    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        IsOpen = true;
    }

    /// <summary>指针离开事件处理</summary>
    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        if (_popup?.IsPointerOverPopup != true)
            IsOpen = false;
    }

    /// <summary>关闭当前子菜单及其所有父级菜单</summary>
    public void CloseAllMenus()
    {
        IsOpen = false;
        var parent = this.Parent;
        while (parent is not null)
        {
            if (parent is CyanMenubarMenu menu)
            {
                menu.IsOpen = false;
                return;
            }
            parent = parent.Parent;
        }
    }

    private void OnOpenChanged()
    {
        PseudoClasses.Set(":open", IsOpen);
    }
}

/// <summary>菜单栏分隔符控件</summary>
public class CyanMenubarSeparator : TemplatedControl
{
}
