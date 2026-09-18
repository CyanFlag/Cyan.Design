using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Metadata;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Selections;

/// <summary>分段选择器项控件，作为分段选择器的单个选项</summary>
public class CyanSegmentedItem : TemplatedControl
{
    /// <summary>标签内容样式属性</summary>
    public static readonly StyledProperty<object?> LabelProperty =
        AvaloniaProperty.Register<CyanSegmentedItem, object?>(nameof(Label));

    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanSegmentedItem, object?>(nameof(Icon));

    /// <summary>选项值样式属性</summary>
    public static readonly StyledProperty<object?> ValueProperty =
        AvaloniaProperty.Register<CyanSegmentedItem, object?>(nameof(Value));

    /// <summary>是否禁用样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanSegmentedItem, bool>(nameof(Disabled));

    /// <summary>是否选中样式属性</summary>
    public static readonly StyledProperty<bool> IsSelectedProperty =
        AvaloniaProperty.Register<CyanSegmentedItem, bool>(nameof(IsSelected));

    static CyanSegmentedItem()
    {
        IsSelectedProperty.Changed.AddClassHandler<CyanSegmentedItem>((item, _) => item.UpdatePseudoClasses());
        DisabledProperty.Changed.AddClassHandler<CyanSegmentedItem>((item, _) => item.UpdatePseudoClasses());
    }

    /// <summary>标签内容</summary>
    [Content]
    public object? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>图标</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>选项值</summary>
    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool Disabled
    {
        get => GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>是否选中</summary>
    public bool IsSelected
    {
        get => GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    internal CyanSegmented? ParentSegmented { get; set; }

    internal void SetShapePseudoClasses(bool isRound, bool isVertical)
    {
        PseudoClasses.Set(":round", isRound);
        PseudoClasses.Set(":vertical", isVertical);
    }

    /// <summary>指针按下事件处理</summary>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (Disabled || ParentSegmented?.Disabled == true)
            return;
        ParentSegmented?.SelectItem(this);
        e.Handled = true;
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdatePseudoClasses();
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":selected", IsSelected);
        PseudoClasses.Set(":disabled", Disabled);
    }
}
