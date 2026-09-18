using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Dropdowns;

/// <summary>下拉菜单项控件</summary>
public class CyanDropdownItem : Button
{
    /// <summary>菜单项键值样式属性</summary>
    public static readonly StyledProperty<object?> KeyProperty =
        AvaloniaProperty.Register<CyanDropdownItem, object?>(nameof(Key));

    /// <summary>菜单项图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanDropdownItem, object?>(nameof(Icon));

    /// <summary>危险状态样式属性</summary>
    public static readonly StyledProperty<bool> DangerProperty =
        AvaloniaProperty.Register<CyanDropdownItem, bool>(nameof(Danger));

    /// <summary>分隔线样式属性</summary>
    public static readonly StyledProperty<bool> DividerProperty =
        AvaloniaProperty.Register<CyanDropdownItem, bool>(nameof(Divider));

    /// <summary>选中状态样式属性</summary>
    public static readonly StyledProperty<bool> SelectedProperty =
        AvaloniaProperty.Register<CyanDropdownItem, bool>(nameof(Selected));

    /// <summary>菜单项点击路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ItemClickEvent =
        RoutedEvent.Register<CyanDropdownItem, RoutedEventArgs>(nameof(ItemClick), RoutingStrategies.Bubble);

    static CyanDropdownItem()
    {
        DangerProperty.Changed.AddClassHandler<CyanDropdownItem>((i, _) => i.UpdatePseudoClasses());
        DividerProperty.Changed.AddClassHandler<CyanDropdownItem>((i, _) => i.UpdatePseudoClasses());
        SelectedProperty.Changed.AddClassHandler<CyanDropdownItem>((i, _) => i.UpdatePseudoClasses());
    }

    /// <summary>菜单项键值</summary>
    public object? Key
    {
        get => GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    /// <summary>菜单项图标</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>是否处于危险状态</summary>
    public bool Danger
    {
        get => GetValue(DangerProperty);
        set => SetValue(DangerProperty, value);
    }

    /// <summary>是否显示为分隔线</summary>
    public bool Divider
    {
        get => GetValue(DividerProperty);
        set => SetValue(DividerProperty, value);
    }

    /// <summary>是否处于选中状态</summary>
    public bool Selected
    {
        get => GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }

    /// <summary>菜单项点击事件</summary>
    public event EventHandler<RoutedEventArgs>? ItemClick
    {
        add => AddHandler(ItemClickEvent, value);
        remove => RemoveHandler(ItemClickEvent, value);
    }

    /// <summary>点击事件处理</summary>
    protected override void OnClick()
    {
        base.OnClick();
        RaiseEvent(new RoutedEventArgs(ItemClickEvent));
        var visual = this.GetVisualParent();
        while (visual != null)
        {
            if (visual is Popup popup)
            {
                popup.IsOpen = false;
                break;
            }
            visual = visual.GetVisualParent();
        }
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":danger", Danger);
        PseudoClasses.Set(":divider", Divider);
        PseudoClasses.Set(":selected", Selected);
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdatePseudoClasses();
    }
}
