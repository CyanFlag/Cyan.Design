using System.Collections;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Display;

/// <summary>折叠容器控件，可包含多个折叠面板</summary>
public class CyanCollapse : ItemsControl
{
    /// <summary>是否手风琴模式样式属性</summary>
    public static readonly StyledProperty<bool> AccordionProperty =
        AvaloniaProperty.Register<CyanCollapse, bool>(nameof(Accordion));

    /// <summary>是否显示边框样式属性</summary>
    public static readonly StyledProperty<bool> BorderedProperty =
        AvaloniaProperty.Register<CyanCollapse, bool>(nameof(Bordered), true);

    /// <summary>是否幽灵模式样式属性</summary>
    public static readonly StyledProperty<bool> GhostProperty =
        AvaloniaProperty.Register<CyanCollapse, bool>(nameof(Ghost));

    /// <summary>尺寸样式属性</summary>
    public static readonly StyledProperty<CollapseSize> SizeProperty =
        AvaloniaProperty.Register<CyanCollapse, CollapseSize>(nameof(Size), CollapseSize.Medium);

    /// <summary>展开图标位置样式属性</summary>
    public static readonly StyledProperty<CollapseIconPlacement> ExpandIconPlacementProperty =
        AvaloniaProperty.Register<CyanCollapse, CollapseIconPlacement>(nameof(ExpandIconPlacement), CollapseIconPlacement.Start);

    static CyanCollapse()
    {
        ItemsPanelProperty.OverrideDefaultValue<CyanCollapse>(
            new FuncTemplate<Panel>(() => new StackPanel { Orientation = Orientation.Vertical }));
    }

    /// <summary>是否手风琴模式，仅允许单个面板展开</summary>
    public bool Accordion
    {
        get => GetValue(AccordionProperty);
        set => SetValue(AccordionProperty, value);
    }

    /// <summary>是否显示边框</summary>
    public bool Bordered
    {
        get => GetValue(BorderedProperty);
        set => SetValue(BorderedProperty, value);
    }

    /// <summary>是否幽灵模式，无边框背景</summary>
    public bool Ghost
    {
        get => GetValue(GhostProperty);
        set => SetValue(GhostProperty, value);
    }

    /// <summary>尺寸</summary>
    public CollapseSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>展开图标位置</summary>
    public CollapseIconPlacement ExpandIconPlacement
    {
        get => GetValue(ExpandIconPlacementProperty);
        set => SetValue(ExpandIconPlacementProperty, value);
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdatePseudoClasses();
        SyncPanels();
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":bordered", Bordered);
        PseudoClasses.Set(":ghost", Ghost);
        PseudoClasses.Set(":large", Size == CollapseSize.Large);
        PseudoClasses.Set(":small", Size == CollapseSize.Small);
    }

    private void SyncPanels()
    {
        foreach (var panel in GetPanels())
        {
            panel.ParentCollapse = this;
            panel.Size = Size;
            panel.ExpandIconPlacement = ExpandIconPlacement;
        }
    }

    internal void OnPanelExpandedChanged(CyanCollapsePanel panel, bool isExpanded)
    {
        if (Accordion && isExpanded)
        {
            foreach (var p in GetPanels())
            {
                if (p != panel && p.IsExpanded)
                    p.IsExpanded = false;
            }
        }
    }

    private IEnumerable<CyanCollapsePanel> GetPanels()
    {
        if (Items is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                if (item is CyanCollapsePanel panel)
                    yield return panel;
            }
        }
    }

    /// <summary>属性改变事件处理</summary>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == BorderedProperty || change.Property == GhostProperty ||
            change.Property == SizeProperty)
        {
            UpdatePseudoClasses();
            if (change.Property == SizeProperty)
                SyncPanels();
        }
        if (change.Property == ExpandIconPlacementProperty)
            SyncPanels();
    }
}
