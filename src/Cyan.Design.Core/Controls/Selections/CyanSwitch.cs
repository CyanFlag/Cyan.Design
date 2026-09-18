using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.VisualTree;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Selections;

/// <summary>开关控件，支持选中与未选中状态的子内容显示</summary>
public class CyanSwitch : ToggleButton
{
    /// <summary>选中时显示内容样式属性</summary>
    public static readonly StyledProperty<object?> CheckedChildrenProperty =
        AvaloniaProperty.Register<CyanSwitch, object?>(nameof(CheckedChildren));

    /// <summary>未选中时显示内容样式属性</summary>
    public static readonly StyledProperty<object?> UncheckedChildrenProperty =
        AvaloniaProperty.Register<CyanSwitch, object?>(nameof(UncheckedChildren));

    /// <summary>尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> SizeProperty =
        AvaloniaProperty.Register<CyanSwitch, ControlSize>(nameof(Size), ControlSize.Middle);

    /// <summary>是否加载中样式属性</summary>
    public static readonly StyledProperty<bool> LoadingProperty =
        AvaloniaProperty.Register<CyanSwitch, bool>(nameof(Loading));

    static CyanSwitch()
    {
        LoadingProperty.Changed.AddClassHandler<CyanSwitch>((s, _) => s.UpdatePseudoClasses());
        SizeProperty.Changed.AddClassHandler<CyanSwitch>((s, _) => s.UpdatePseudoClasses());
    }

    /// <summary>选中时显示内容</summary>
    public object? CheckedChildren
    {
        get => GetValue(CheckedChildrenProperty);
        set => SetValue(CheckedChildrenProperty, value);
    }

    /// <summary>未选中时显示内容</summary>
    public object? UncheckedChildren
    {
        get => GetValue(UncheckedChildrenProperty);
        set => SetValue(UncheckedChildrenProperty, value);
    }

    /// <summary>尺寸</summary>
    public ControlSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>是否加载中</summary>
    public bool Loading
    {
        get => GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdatePseudoClasses();
    }

    /// <summary>点击事件处理</summary>
    protected override void OnClick()
    {
        if (Loading || !IsEnabled)
            return;
        base.OnClick();
    }

    /// <summary>键盘按下事件处理</summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (Loading || !IsEnabled)
        {
            if (e.Key is Key.Space or Key.Enter)
            {
                e.Handled = true;
                return;
            }
        }
        base.OnKeyDown(e);
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":small", Size == ControlSize.Small);
        PseudoClasses.Set(":large", Size == ControlSize.Large);
        PseudoClasses.Set(":loading", Loading);
    }
}
