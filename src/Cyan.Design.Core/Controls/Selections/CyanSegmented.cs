using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.VisualTree;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Selections;

/// <summary>分段选择器控件，用于在多个选项中进行单选</summary>
public class CyanSegmented : ItemsControl
{
    static CyanSegmented()
    {
        ItemsPanelProperty.OverrideDefaultValue<CyanSegmented>(
            new FuncTemplate<Panel>(() => new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal }));
    }

    /// <summary>选中值样式属性</summary>
    public static readonly StyledProperty<object?> SelectedValueProperty =
        AvaloniaProperty.Register<CyanSegmented, object?>(nameof(SelectedValue), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    /// <summary>尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> SizeProperty =
        AvaloniaProperty.Register<CyanSegmented, ControlSize>(nameof(Size), ControlSize.Middle);

    /// <summary>方向样式属性</summary>
    public static readonly StyledProperty<SegmentedOrientation> OrientationProperty =
        AvaloniaProperty.Register<CyanSegmented, SegmentedOrientation>(nameof(Orientation), SegmentedOrientation.Horizontal);

    /// <summary>是否块级布局样式属性</summary>
    public static readonly StyledProperty<bool> BlockProperty =
        AvaloniaProperty.Register<CyanSegmented, bool>(nameof(Block));

    /// <summary>形状样式属性</summary>
    public static readonly StyledProperty<SegmentedShape> ShapeProperty =
        AvaloniaProperty.Register<CyanSegmented, SegmentedShape>(nameof(Shape), SegmentedShape.Default);

    /// <summary>是否禁用样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanSegmented, bool>(nameof(Disabled));

    /// <summary>选中值</summary>
    public object? SelectedValue
    {
        get => GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }

    /// <summary>尺寸</summary>
    public ControlSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>方向，水平或垂直排列</summary>
    public SegmentedOrientation Orientation
    {
        get => GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>是否块级布局，撑满父容器</summary>
    public bool Block
    {
        get => GetValue(BlockProperty);
        set => SetValue(BlockProperty, value);
    }

    /// <summary>形状</summary>
    public SegmentedShape Shape
    {
        get => GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool Disabled
    {
        get => GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        ApplyItemsPanel();
        base.OnApplyTemplate(e);
    }


    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        ApplyItemsPanel();
        UpdatePseudoClasses();
        SyncItems();
        InitializeSelection();
    }

    private void ApplyItemsPanel()
    {
        var orientation = Orientation == SegmentedOrientation.Horizontal
            ? Avalonia.Layout.Orientation.Horizontal
            : Avalonia.Layout.Orientation.Vertical;

        ItemsPanel = new FuncTemplate<Panel>(() => new StackPanel { Orientation = orientation });

        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        {
            foreach (var descendant in this.GetVisualDescendants())
            {
                if (descendant.GetType().Name != "ItemsPresenter")
                    continue;
                var panel = descendant.GetVisualChildren().OfType<StackPanel>().FirstOrDefault();
                if (panel != null)
                    panel.Orientation = orientation;
                break;
            }
        });
    }

    internal void SelectItem(CyanSegmentedItem item)
    {
        foreach (var it in GetItems())
            it.IsSelected = it == item;
        SelectedValue = item.Value;
    }

    private void SyncItems()
    {
        foreach (var item in GetItems())
        {
            item.ParentSegmented = this;
            if (Disabled)
                item.IsEnabled = false;
            item.SetShapePseudoClasses(
                Shape == SegmentedShape.Round,
                Orientation == SegmentedOrientation.Vertical);
        }
    }

    private void InitializeSelection()
    {
        var items = GetItems().ToList();
        if (items.Count == 0) return;

        if (SelectedValue != null)
        {
            foreach (var item in items)
                item.IsSelected = ValuesEqual(item.Value, SelectedValue);
        }

        if (!items.Any(i => i.IsSelected))
        {
            var first = items.FirstOrDefault(i => !i.Disabled) ?? items[0];
            first.IsSelected = true;
            SelectedValue = first.Value;
        }
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":large", Size == ControlSize.Large);
        PseudoClasses.Set(":small", Size == ControlSize.Small);
        PseudoClasses.Set(":vertical", Orientation == SegmentedOrientation.Vertical);
        PseudoClasses.Set(":block", Block);
        PseudoClasses.Set(":round", Shape == SegmentedShape.Round);
        PseudoClasses.Set(":disabled", Disabled);
    }

    private IEnumerable<CyanSegmentedItem> GetItems()
    {
        if (Items is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                if (item is CyanSegmentedItem segItem)
                    yield return segItem;
            }
        }
    }

    private static bool ValuesEqual(object? a, object? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b) || a.ToString() == b.ToString();
    }

    /// <summary>属性改变事件处理</summary>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == SizeProperty || change.Property == OrientationProperty ||
            change.Property == BlockProperty || change.Property == ShapeProperty ||
            change.Property == DisabledProperty)
        {
            UpdatePseudoClasses();
            if (change.Property == OrientationProperty)
            {
                ApplyItemsPanel();
                SyncItems();
            }
            else if (change.Property == DisabledProperty || change.Property == ShapeProperty)
                SyncItems();
        }
        if (change.Property == SelectedValueProperty)
            UpdateSelection();
    }

    private void UpdateSelection()
    {
        foreach (var item in GetItems())
            item.IsSelected = ValuesEqual(item.Value, SelectedValue);
    }
}
