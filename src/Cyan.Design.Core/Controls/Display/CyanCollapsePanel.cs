using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Metadata;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Display;

/// <summary>折叠面板项控件，作为折叠容器的单个面板</summary>
public class CyanCollapsePanel : TemplatedControl
{
    /// <summary>头部内容样式属性</summary>
    public static readonly StyledProperty<object?> HeaderProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, object?>(nameof(Header));

    /// <summary>主体内容样式属性</summary>
    public static readonly StyledProperty<object?> ContentProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, object?>(nameof(Content));

    /// <summary>额外内容样式属性，显示在头部右侧</summary>
    public static readonly StyledProperty<object?> ExtraProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, object?>(nameof(Extra));

    /// <summary>面板键名样式属性，用于唯一标识</summary>
    public static readonly StyledProperty<string?> KeyProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, string?>(nameof(Key));

    /// <summary>是否显示展开箭头样式属性</summary>
    public static readonly StyledProperty<bool> ShowArrowProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, bool>(nameof(ShowArrow), true);

    /// <summary>是否展开样式属性</summary>
    public static readonly StyledProperty<bool> IsExpandedProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, bool>(nameof(IsExpanded));

    /// <summary>是否禁用样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, bool>(nameof(Disabled));

    /// <summary>可折叠区域样式属性</summary>
    public static readonly StyledProperty<CollapseCollapsible> CollapsibleProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, CollapseCollapsible>(nameof(Collapsible), CollapseCollapsible.Header);

    /// <summary>展开图标位置样式属性</summary>
    public static readonly StyledProperty<CollapseIconPlacement> ExpandIconPlacementProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, CollapseIconPlacement>(nameof(ExpandIconPlacement), CollapseIconPlacement.Start);

    /// <summary>面板尺寸样式属性</summary>
    public static readonly StyledProperty<CollapseSize> SizeProperty =
        AvaloniaProperty.Register<CyanCollapsePanel, CollapseSize>(nameof(Size), CollapseSize.Medium);

    private Control? _headerArea;
    private Control? _arrowArea;

    static CyanCollapsePanel()
    {
        IsExpandedProperty.Changed.AddClassHandler<CyanCollapsePanel>((p, _) => p.OnIsExpandedChanged());
        DisabledProperty.Changed.AddClassHandler<CyanCollapsePanel>((p, _) => p.UpdatePseudoClasses());
        CollapsibleProperty.Changed.AddClassHandler<CyanCollapsePanel>((p, _) => p.UpdatePseudoClasses());
        ExpandIconPlacementProperty.Changed.AddClassHandler<CyanCollapsePanel>((p, _) => p.UpdatePseudoClasses());
        SizeProperty.Changed.AddClassHandler<CyanCollapsePanel>((p, _) => p.UpdatePseudoClasses());
    }

    /// <summary>头部内容</summary>
    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>主体内容</summary>
    [Content]
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>额外内容，显示在头部右侧</summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }

    /// <summary>面板键名，用于唯一标识</summary>
    public string? Key
    {
        get => GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    /// <summary>是否显示展开箭头</summary>
    public bool ShowArrow
    {
        get => GetValue(ShowArrowProperty);
        set => SetValue(ShowArrowProperty, value);
    }

    /// <summary>是否展开</summary>
    public bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool Disabled
    {
        get => GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>可折叠区域，指定可点击展开的范围</summary>
    public CollapseCollapsible Collapsible
    {
        get => GetValue(CollapsibleProperty);
        set => SetValue(CollapsibleProperty, value);
    }

    /// <summary>展开图标位置</summary>
    public CollapseIconPlacement ExpandIconPlacement
    {
        get => GetValue(ExpandIconPlacementProperty);
        set => SetValue(ExpandIconPlacementProperty, value);
    }

    /// <summary>面板尺寸</summary>
    public CollapseSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    internal CyanCollapse? ParentCollapse { get; set; }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (_headerArea != null)
            _headerArea.PointerPressed -= OnHeaderPointerPressed;
        if (_arrowArea != null)
            _arrowArea.PointerPressed -= OnHeaderPointerPressed;

        _headerArea = e.NameScope.Find<Control>("PART_Header");
        _arrowArea = e.NameScope.Find<Control>("PART_Arrow");

        if (_headerArea != null)
            _headerArea.PointerPressed += OnHeaderPointerPressed;
        if (_arrowArea != null)
            _arrowArea.PointerPressed += OnHeaderPointerPressed;

        UpdatePseudoClasses();
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdatePseudoClasses();
    }

    private void OnIsExpandedChanged()
    {
        PseudoClasses.Set(":expanded", IsExpanded);
        ParentCollapse?.OnPanelExpandedChanged(this, IsExpanded);
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":expanded", IsExpanded);
        PseudoClasses.Set(":disabled", Disabled || Collapsible == CollapseCollapsible.Disabled);
        PseudoClasses.Set(":collapsible-icon", Collapsible == CollapseCollapsible.Icon);
        PseudoClasses.Set(":iconend", ExpandIconPlacement == CollapseIconPlacement.End);
        PseudoClasses.Set(":large", Size == CollapseSize.Large);
        PseudoClasses.Set(":small", Size == CollapseSize.Small);
    }

    private void OnHeaderPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (Disabled || Collapsible == CollapseCollapsible.Disabled)
            return;

        if (Collapsible == CollapseCollapsible.Icon && sender == _headerArea)
            return;

        IsExpanded = !IsExpanded;
        e.Handled = true;
    }
}
